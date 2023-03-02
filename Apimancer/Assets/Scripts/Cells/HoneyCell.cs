using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyCell : Cell
{
    private CellType type = CellType.HONEY;
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
