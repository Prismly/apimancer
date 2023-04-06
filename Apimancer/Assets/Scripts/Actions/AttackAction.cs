using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    public Unit target;

    public AttackAction(ref Unit from, ref Unit to, uint range = 1, uint cost = 0)
        : base(ActionType.ATTACK, from, range, cost)
    {

        target = to;
    }

    public override bool Execute(Cell cell) 
    {
        Unit.DamageTarget(unit.AttackDamage, target);
        return true;
    }
}
