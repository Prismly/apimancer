using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTrap : SpellAction
{
    public static string sName = "Honey Trap";
    public static uint sRange = 4;
    public static int sCost = 3;

    public HoneyTrap(Unit from)
        :base(from, sRange, sCost)
    {
    }

    public override bool Validate(Cell cell)
    {
        Unit u = cell.Occupant as Unit;
        if (u != null)
        {
            Unit.UnitType t = u.Type;
            switch (t)
            {
                case Unit.UnitType.BEE_WORKER:
                case Unit.UnitType.BEE_MINING:
                case Unit.UnitType.BEE_BUMBLE:
                case Unit.UnitType.ANT_WIZARD:
                case Unit.UnitType.ANT_ARMY:
                case Unit.UnitType.ANT_FIRE:
                case Unit.UnitType.ANT_WORKER:
                    return true;
                default:
                    return false;
            }
        }
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
        w.SetAnimState(Entity.AnimState.WIZ_SPELLCAST);
        CellManager.Instance.ReplaceCell(cell.Location, CellType.HONEY);
        CellManager.Instance.GetCell(cell.Location).SetColor(new Color(1, 1, 1, 1));
        return true;
    }
}
