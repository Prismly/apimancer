using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCell : Cell
{
    private CellType type = CellType.LAVA;
    public override CellType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public override void OnEndTurn() {
        Unit u = Occupant as Unit;
        if (u != null) {
            if (u.Type == Unit.UnitType.ANT_FIRE) {
                Unit.DamageTarget(-1, u);
            }
            else {
                Unit.DamageTarget(1, u);
                Debug.Log("Hello");
            }
        }
    }
}
