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

    protected override void OnEnter() {
        Unit u = this.Occupant as Unit;
        if (u != null) {
            Unit.DamageTarget(1, u);
        }
    }
}
