using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bee : Unit
{
    public enum BeeAnimState 
    { 
        IDLE = 0,
        STING,
        DEATH
    };

    public override void AttackTarget(int dmg, Unit target)
    {
        if (target.UnitFaction == Unit.Faction.RESOURCE) {
            int m = (target.Health >= dmg) ?
                      (dmg) : (target.Health);
            Commander.addMana(m);
        }
        target.ReceiveDamage(dmg);
    }
}
