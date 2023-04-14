using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public abstract int AttackDamage { get; set; }
    public abstract int MovementSpeed { get; set; }
    public abstract List<Faction> TargetPriorities { get; set; }
    public abstract IEnumerator DetermineMovement();

    public virtual Action DetermineAction() { return null; }

    // static deal damage to target
    public static void DamageTarget(int dmg, Unit target)
    {
        target.ReceiveDamage(dmg);
    }

    // member deal damage to target
    public void AttackTarget(int dmg, Unit target)
    {
        if (target.UnitFaction == Unit.Faction.RESOURCE)
        {
            int m = (target.Health >= dmg) ?
                      (dmg) : (target.Health);
            Commander.AddMana(m);
        }
        target.ReceiveDamage(dmg);
        PlayAnimation(Entity.AnimState.ACTION);
    }

    // receive damage
    public virtual void ReceiveDamage(int dmg) 
    {
        this.Health -= dmg;
        if (this.Health <= 0)
            PlayAnimation(Entity.AnimState.DEATH);
    }

    public virtual void setLocation(Vector2Int location)
    {
        this.setLocation(CellManager.Instance.GetCell(location));
    }

    public virtual void setLocation(Cell cell) 
    {
        if (cell != null && !cell.IsOccupied) {
            cell.Occupant = this;
            this.loc = cell.Location;
            this.transform.position = cell.transform.position + new Vector3(0, 0, -0.04f);
            this.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    public virtual void OnDeath()
    {
        GameManager.Instance.Kill(this);
        if (Commander != null && Commander.Units.Contains(this))
        {
            Commander.Units.Remove(this);
        }
        GetCell().Occupant = null;
        Destroy(this.gameObject, 1.0f);

        // End game if wizard
    }

    public override void OnSelect() {}
    public override void OnDeselect() {}
    public override void OnHover()
    {
        if (UnitFaction != Faction.RESOURCE && UnitFaction != Faction.OTHER)
        {
            UIManager.Instance.ShowHealthBox(this);
        }
    }
    public override void OnUnhover()
    {
        UIManager.Instance.HideHealthBox();
    }

    public void setStatus(Status.Condition condition, short duration) {
        switch (condition) {
            case Status.Condition.HONEYED:
                this.condition = new Honeyed(this, duration);
                break;
            case Status.Condition.BURNED:
                this.condition = new Burned(this, duration);
                break;
            case Status.Condition.WET:
                this.condition = new Wet(this, duration);
                break;
            default:
                this.condition = null;
                break;
        }
    }

    public void doStatus() {
        condition.doStatus();
    }

    protected Tuple<Unit, int, List<Cell>> FindClosestTarget(List<Unit> targets)
    {
        Unit t = null;
        int dist = int.MaxValue;
        List<Cell> path = null;
        foreach (Unit u in targets)
        {
            List<Cell> tempPath = Entity.PathFind(this, u);
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

    public virtual Tuple<Unit, int, List<Cell>> DetermineTarget()
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
                if (pTarget == null) pTarget = tempTarget;
                if (tempTarget.Item2 < MovementSpeed)
                {
                    target = tempTarget;
                    break;
                }
            }
        }

        Tuple<Unit, int, List<Cell>> finalTarget = target != null ? target : pTarget;
        return finalTarget;
    }

    public virtual void PlayAnimation(Entity.AnimState a) {
        animator.SetInteger("state", (int)a);
    }

    protected void RelinquishControl() {
        PlayAnimation(Entity.AnimState.IDLE);
        GameManager.Instance.NotifyNextUnit();
    }
}
