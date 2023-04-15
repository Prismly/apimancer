using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTrap : SpellAction
{
    public HoneyTrap(Unit from, uint range, int cost)
        :base(from, range, cost)
    {
    }

    public override bool Execute(Cell cell)
    {
        Debug.Log("HONEY TRAP!");
        GameManager.Instance.Wizards[0].setMana(GameManager.Instance.Wizards[0].getMana() - cost);
        if (!Validate(cell) && cell.Type != CellType.DIRT)
            return false;

        if (!cell.IsOccupied)
            cell.SetColor(new Color(1.0f, 0.3f, 0));

        //Unit target = cell.Occupant as Unit;
        //target.SetSticky(true);
        return true;
    }
}
