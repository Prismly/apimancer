using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTrap : SpellAction
{
    public HoneyTrap(Unit from, uint range, int cost)
        :base(from, range, cost)
    {
    }

    public override bool Validate(Cell cell)
    {
        return true;
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

        CellManager.Instance.ReplaceCell(cell.Location, CellType.HONEY);
        CellManager.Instance.GetCell(cell.Location).SetColor(new Color(1, 1, 1, 1));
        return true;
    }
}
