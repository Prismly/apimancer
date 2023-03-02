using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCell : Cell
{
    private CellType type = CellType.WALL;
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
}
