using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyCell : Cell
{
    private CellType type = CellType.HONEY;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public override void OnEnter()
    {
        Unit u = Occupant as Unit;
        if (u != null)
        {
            if (u.condition == null || u.condition.condition == Status.Condition.HONEYED)
                u.setStatus(Status.Condition.HONEYED, 1);
            else u.setStatus(Status.Condition.NONE);
        }
    }

    public override void OnEndTurn()
    {
        Unit u = Occupant as Unit;
        if (u != null)
        {
            if (u.condition == null || u.condition.condition == Status.Condition.HONEYED)
                u.setStatus(Status.Condition.HONEYED, 1);
            else u.setStatus(Status.Condition.NONE);
        }
    }
}
