using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : SpellAction
{
    public Teleport(Unit from, uint range, short cost)
        : base(from, range, cost)
    {
    }

    public override bool Execute(Cell cell)
    {
        Debug.Log("TELEPORT!");
        if (!Validate(cell))
            return false;

        if (cell.IsOccupied)
            return false;

        unit.setLocation(cell);
        return true;
    }
}
