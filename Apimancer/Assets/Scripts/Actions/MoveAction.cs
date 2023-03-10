using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public MoveAction(ref Unit unit)
        : base(ref unit)
    {

    }
    public override bool Execute(Cell cell)
    {
        return unit.MoveToCell(cell);
    }
}
