using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : SpellAction
{
    public Teleport(Unit from, uint range, int cost)
        : base(from, range, cost)
    {
    }

    public override bool Validate(Cell cell)
    {
        return !cell.IsOccupied;

        //if (cell.IsOccupied)
        //    return false;

        //List<Cell> inRange = unit.GetCell().GetCellsRange(3);
        //foreach (Cell c in inRange)
        //{
        //    if (c.Location == cell.Location)
        //    {
        //        Wizard w = (Wizard)unit;
        //        if (w.SpendMana(cost))
        //        {
        //            return true;
        //        }
        //    }
        //}
        //return false;
    }

    public override bool Execute(Cell cell)
    {
        Debug.Log("TELEPORT!");

        if (!Validate(cell))
            return false;

        Wizard w = (Wizard)unit;
        if (!w.SpendMana(cost))
        {
            return false;
        }

        unit.GetCell().Occupant = null;
        unit.setLocation(cell);
        return true;
    }
}
