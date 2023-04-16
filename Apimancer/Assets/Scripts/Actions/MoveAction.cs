using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public MoveAction(Unit unit)
        : base(ActionType.MOVE, unit, 2, 0)
    {

    }

    public override bool Validate(Cell cell)
    {
        return !cell.IsOccupied;
    }

    public override bool Execute(Cell cell)
    {
        return unit.MoveToCell(cell);
    }
}
