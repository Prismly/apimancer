using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCell : Cell
{
    private CellType type = CellType.DIRT;
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
