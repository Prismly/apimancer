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
        ANT
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
        RESOURCE_FLOWER
    }

    public Faction UnitFaction;
    public UnitType Type;
    public UnitState State = UnitState.IDLE;
    public Wizard Commander;
    public Status condition;
    public abstract int MaxHealth { get; set; }
    public abstract int Health { get; set; }
    public abstract int AttackDamage { get; set; }
    public abstract int MovementSpeed { get; set; }
    public abstract IEnumerator DetermineMovement();
    public abstract Action DetermineAction();

    // static deal damage to target
    public static void DamageTarget(int dmg, Unit target)
    {
        target.ReceiveDamage(dmg);
    }

    // member deal damage to target
    public virtual void AttackTarget(int dmg, Unit target)
    {
        target.ReceiveDamage(dmg);
    }

    // receive damage
    public virtual void ReceiveDamage(int dmg) 
    {
        this.Health -= dmg;
        if (this.Health <= 0)
        {
            OnDeath();
        }
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

    protected Tuple<Unit, short, List<Cell>> FindClosestTarget(List<Unit> targets)
    {
        Unit t = null;
        short dist = short.MaxValue;
        List<Cell> path = null;
        foreach (Unit u in targets)
        {
            List<Cell> tempPath = Entity.PathFind(this, u);
            if (tempPath.Count < dist)
            {
                t = u;
                dist = (short)tempPath.Count;
                path = tempPath;
            }
        }
        return new Tuple<Unit, short, List<Cell>>(t, dist, path);
    }

    public virtual void OnDeath()
    {
        PlayDeathAnim();
        GameManager.Instance.Kill(this);
        if (Commander != null && Commander.Units.Contains(this))
        {
            Commander.Units.Remove(this);
        }
        GetCell().Occupant = null;
        Destroy(this.gameObject, 1.0f);

        // End game if wizard
    }

    public override void OnSelect(){}
    public override void OnDeselect(){}
    public override void OnHover()
    {
        UIManager.Instance.ShowHealthBox(this);
    }
    public override void OnUnhover()
    {
        UIManager.Instance.HideHealthBox();
    }

    protected virtual void PlayDeathAnim() { return; }

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
}
