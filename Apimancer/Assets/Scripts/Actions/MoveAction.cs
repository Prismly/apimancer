using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public MoveAction(Unit unit)
        : base(unit, 0, 0)
    {

    }
    public override bool Execute(Cell cell)
    {
        return unit.MoveToCell(cell);
    }
}
