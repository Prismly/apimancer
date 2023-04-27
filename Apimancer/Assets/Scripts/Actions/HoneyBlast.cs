using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlast : SpellAction
{
    public static string sName = "Honey Blast";
    public static uint sRange = 4;
    public static int sCost = 3;

    public HoneyBlast(Unit from)
        :base(from, sRange, sCost)
    {
    }

    public override bool Validate(Cell cell)
    {
        Unit u = cell.Occupant as Unit;
        if (u != null) {
            Unit.UnitType t = u.Type;
            switch (t) {
                case Unit.UnitType.BEE_WORKER:
                case Unit.UnitType.BEE_MINING:
                case Unit.UnitType.BEE_BUMBLE:
                case Unit.UnitType.ANT_WIZARD:
                case Unit.UnitType.ANT_ARMY:
                case Unit.UnitType.ANT_FIRE:
                case Unit.UnitType.ANT_WORKER:
                    return true;
            }
        }
        return false;
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
        ((Unit)cell.Occupant).setStatus(Status.Condition.HONEYED, 2);
        return true;
    }
}
