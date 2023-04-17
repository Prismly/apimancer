using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public MoveAction(Unit unit, uint dist)
        : base(ActionType.MOVE, unit, dist, 0)
    {

    }

    public override bool Validate(Cell cell)
    {
        return !cell.IsOccupied;
    }

    public override bool Execute(Cell cell)
    {
        HumanWizard player = unit as HumanWizard;
        if (player != null)
        {
            player.hasMoved = true;
        }
        return unit.MoveToCell(cell);
    }
}
