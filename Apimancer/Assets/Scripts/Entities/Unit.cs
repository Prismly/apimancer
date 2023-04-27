using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity
{
    public enum UnitState
    {
        SPAWN,
        IDLE,
        ATTACK,
        HARVEST,
        DEATH
    }

    public enum Faction
    {
        RESOURCE,
        BEE,
        ANT,
        OTHER
    }

    public enum UnitType
    {
        BEE_WIZARD,
        BEE_WORKER,
        BEE_MINING,
        BEE_BUMBLE,
        ANT_WIZARD,
        ANT_WORKER,
        ANT_FIRE,
        ANT_ARMY,
        RESOURCE_FLOWER,
        OTHER_BOULDER
    }

    public string unitName;

    public Faction UnitFaction;
    public UnitType Type;
    public UnitState State = UnitState.IDLE;
    public Wizard Commander;
    public Status condition;
    public abstract int MaxHealth { get; set; }
    public abstract int Health { get; set; }
    public virtual int AttackDamage { get; set; }
    public virtual int AttackRange { get; set; }
    public virtual int MovementSpeed { get; set; }
    public virtual List<Faction> TargetPriorities { get; set; }

    public virtual Action DetermineAction() { return null; }

    public virtual IEnumerator DetermineMovement()
    {
        PlaySound(Sounds.Warcry);
        int speed = MovementSpeed;
        if (condition != null)
        {
            switch (condition.condition)
            {
                case Status.Condition.HONEYED:
                    {
                        switch (Type)
                        {
                            case UnitType.ANT_ARMY:
                            case UnitType.ANT_FIRE:
                            case UnitType.ANT_WORKER:
                            case UnitType.ANT_WIZARD:
                            case UnitType.BEE_WIZARD:
                                speed = 1;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                case Status.Condition.WET:
                    speed--;
                    break;
                default:
                    break;
            }
        }
        Tuple<Unit, int, List<Cell>> target = DetermineTarget(speed);
        if (target != null)
        {
            int slack = (speed + AttackRange) - target.Item2;
            if (slack > 0) {
                yield return StartCoroutine(MoveAlongPathByAmount(target.Item3, speed - slack + 1));
            }
            else {
                yield return StartCoroutine(MoveAlongPathByAmount(target.Item3, speed));
            }
                
            if (slack > 0)
            {
                if (Type == UnitType.ANT_FIRE) {
                    List<Cell> aoe = CellManager.Instance.GetCell(loc).GetAdjacentList();
                    foreach(Cell c in aoe) {
                        Unit t = c.Occupant as Unit;
                        if (t != null) {
                            if (t.Type != UnitType.ANT_FIRE)
                                AttackTarget(AttackDamage, t);
                            t.setStatus(Status.Condition.BURNED, 2);
                        }
                    }
                }
                else {
                    AttackTarget(AttackDamage, target.Item1);
                }
            }
            else RelinquishControl();
        }
        else RelinquishControl();
    }

    // member deal damage to target
    public void AttackTarget(int dmg, Unit target)
    {
        UpdateDirection(target.GetCell());

        if (target.UnitFaction == Unit.Faction.RESOURCE && Type != UnitType.ANT_FIRE)
        {
            int m = (target.Health >= dmg) ?
                      (dmg) : (target.Health);
            Commander.AddMana(m);
            PlaySound(Sounds.Harvest);
        }
        else PlaySound(Sounds.Attack);
        target.ReceiveDamage(dmg);
        SetAnimState(AnimState.UNIT_ACTION);
    }

    // receive damage
    public virtual void ReceiveDamage(int dmg) 
    {
        UIManager.Instance.SpawnDamageIndicator(dmg, transform.position);
        this.Health -= dmg;
        if (this.Health <= 0)
        {
            if (animator != null)
                SetAnimState(AnimState.DEATH);
            
            GameManager.Instance.Kill(this);
            GetCell().Occupant = null;
            Destroy(this.gameObject, 1.0f);

            OnDeath();

            if (Commander != null && Commander.Units.Contains(this))
            {
                Commander.Units.Remove(this);
            }
        }
    }

    public void Heal(int h) {
        if (h + Health > MaxHealth)
            Health = MaxHealth;
        else Health += h;
    }

    public virtual void setLocation(Vector2Int location)
    {
        this.setLocation(CellManager.Instance.GetCell(location));
    }

    public virtual void setLocation(Cell cell) 
    {
        if (cell != null && !cell.IsOccupied) {
            cell.Occupant = this;
            loc = cell.Location;
            transform.position = cell.transform.position + worldOffset;
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    public virtual void OnDeath()
    {
        PlaySound(Sounds.Death);
    }

    public override void OnSelect() 
    {
        PlaySound(Sounds.Selected);
    }
    public override void OnDeselect() {}
    public override void OnHover()
    {
        if (UnitFaction != Faction.OTHER)
        {
            UIManager.Instance.ShowHealthBox(this);
        }
    }
    public override void OnUnhover()
    {
        UIManager.Instance.HideHealthBox();
    }

    public void setStatus(Status.Condition condition, int duration = 1) {
        if (this.condition == null) {
            switch (condition)
            {
                case Status.Condition.HONEYED:
                    this.condition = new Honeyed(this, duration);
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(0.6f, 0.4f, 0.1f, 1);
                    break;
                case Status.Condition.BURNED:
                    this.condition = new Burned(this, duration);
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(0.8f, 0.4f, 0.2f, 1);
                    break;
                case Status.Condition.WET:
                    this.condition = new Wet(this, duration);
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(0.3f, 0.5f, 0.9f, 1);
                    break;
            }
        }
        else if (this.condition.condition == condition) { 
            this.condition.duration += duration;
        }
        else {
            this.condition = null;
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public void doStatus() {
        if (condition != null)
            condition.doStatus();
    }

    protected Tuple<Unit, int, List<Cell>> FindClosestTarget(List<Unit> targets)
    {
        Unit t = null;
        int dist = int.MaxValue;
        List<Cell> path = null;
        foreach (Unit u in targets)
        {
            List<Cell> tempPath;
            if (Type == UnitType.BEE_MINING) {
                tempPath = MiningBee.PathFind(this, u);
            }
            else {
                tempPath = Entity.PathFind(this, u);
            }
            if (tempPath != null && tempPath.Count < dist && tempPath.Count != 0)
            {
                t = u;
                dist = tempPath.Count;
                path = tempPath;
            }
        }
        return new Tuple<Unit, int, List<Cell>>(t, dist, path);
    }

    protected Tuple<Unit, int, List<Cell>> FindClosestTarget(List<Unit> targets, Cell c) {
        Unit t = null;
        int dist = int.MaxValue;
        List<Cell> path = null;
        foreach (Unit u in targets)
        {
            List<Cell> tempPath = Entity.PathFind(u, c);
            if (tempPath != null && tempPath.Count < dist)
            {
                t = u;
                dist = tempPath.Count;
                path = tempPath;
            }
        }

        if (path == null)
            Debug.Log("Path is null!");

        return new Tuple<Unit, int, List<Cell>>(t, dist, path);
    }

    public virtual Tuple<Unit, int, List<Cell>> DetermineTarget(int speed)
    {
        Dictionary<Unit.Faction, List<Unit>> dUnits = GameManager.Instance.Units;
        List<Unit> lUnits = null;
        Tuple<Unit, int, List<Cell>> target = null;
        Tuple<Unit, int, List<Cell>> pTarget = null;
        int n = TargetPriorities.Count;
        for (int i = 0; i < n; i++)
        {
            Unit.Faction f = TargetPriorities[i];
            if (dUnits.ContainsKey(f) && dUnits[f].Count > 0)
            {
                lUnits = dUnits[f];
                Tuple<Unit, int, List<Cell>> tempTarget = FindClosestTarget(lUnits);
                if (pTarget == null && tempTarget.Item1 != null) pTarget = tempTarget;
                if (tempTarget.Item2 < speed)
                {
                    target = tempTarget;
                    break;
                }
            }
        }

        Tuple<Unit, int, List<Cell>> finalTarget = target != null ? target : pTarget;
        return finalTarget;
    }

    protected void RelinquishControl() {
        SetAnimState(AnimState.IDLE);
        GameManager.Instance.NotifyNextUnit();
    }
}
