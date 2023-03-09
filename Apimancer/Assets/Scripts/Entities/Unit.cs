using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity
{
    public enum Faction
    {
        RESOURCE,
        BEE,
        ANT
    }

    public Faction UnitFaction;
    public abstract float MaxHealth { get; set; }
    public abstract float Health { get; set; }
    public abstract float AttackDamage { get; set; }
    public abstract float MovementSpeed { get; set; }
    public abstract void DetermineAction();

    public static Unit CreateUnit(Cell cell, Faction faction, short unitType)
    {
        Unit newUnit;
        switch (faction) {
            case Faction.BEE:
                newUnit = Bee.CreateBee((Bee.BeeType)unitType);
                break;
            case Faction.ANT:
                newUnit = Ant.CreateAnt((Ant.AntType)unitType);
                break;
            case Faction.RESOURCE:
                newUnit = Resource.CreateResource((Resource.ResourceType)unitType);
                break;
            default:
                newUnit = null;
                break;
        }
        newUnit.setLocation(cell);
        return newUnit;
    }

    public Cell GetCell() 
    {
        return CellManager.Instance.GetCell(this.loc);
    }

    // static deal damage to target
    public static void DamageTarget(float dmg, Unit target) {
        target.ReceiveDamage(dmg);
    }

    // receive damage
    protected virtual void ReceiveDamage(float dmg) 
    {
        this.Health -= dmg;
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
}
