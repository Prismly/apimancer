using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlast : SpellAction
{
    public HoneyBlast(Unit from, uint range, int cost)
        :base(from, range, cost)
    {
    }

    public override bool Execute(Cell cell)
    {
        Debug.Log("HONEY BLAAAAST!");
        GameManager.Instance.Wizards[0].AddMana(-cost);
        if (!Validate(cell))
            return false;

        if (!cell.IsOccupied)
            return false;

        Unit target = cell.Occupant as Unit;
        GameManager.Instance.Kill(unit);
        return true;
    }
}
