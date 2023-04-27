using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlast : SpellAction
{
    public HoneyBlast(Unit from, uint range, int cost)
        :base(from, range, cost)
    {
    }

    public override bool Validate(Cell cell)
    {
        return cell.IsOccupied && cell.Occupant is Unit;
    }

    public override bool Execute(Cell cell)
    {
        if (!Validate(cell))
            return false;

        Wizard w = (Wizard)unit;
        if (!w.SpendMana(cost))
        {
            return false;
        }

        ((Unit)cell.Occupant).setStatus(Status.Condition.HONEYED, 2);
        return true;
    }
}
