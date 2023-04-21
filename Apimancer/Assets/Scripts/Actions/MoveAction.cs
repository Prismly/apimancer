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
        // return !cell.IsOccupied;
        if (cell != null && !cell.IsOccupied)
        {
            List<Cell> path = Entity.PathFind(unit, cell);
            return path.Count - 1 <= range;
        }
        return false;
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
