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

    public abstract Faction UnitFaction { get; set; }
    public abstract float MaxHealth { get; set; }
    public abstract float Health { get; set; }
    public abstract float AttackDamage { get; set; }
    public abstract float MovementSpeed { get; set; }

    // static deal damage to target
    public static void DamageTarget(float dmg, Unit target) {
        target.ReceiveDamage(dmg);
    }

    // receive damage
    protected virtual void ReceiveDamage(float dmg) 
    {
        this.Health -= dmg;
    }
}
