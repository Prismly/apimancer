using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCell : Cell
{
    private CellType type = CellType.FLOWER;
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
