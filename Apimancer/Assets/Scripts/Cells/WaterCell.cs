using UnityEngine;

public class WaterCell : Cell
{
    private CellType type = CellType.WATER;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public override void OnEnter()
    {
        Unit u = Occupant as Unit;
        if (u != null) {
            if (u.condition == null || u.condition.condition == Status.Condition.WET)
                u.setStatus(Status.Condition.WET, 1);
            else u.setStatus(Status.Condition.NONE);
        }
    }

    public override void OnEndTurn()
    {
        Unit u = Occupant as Unit;
        if (u != null)
        {
            if (u.condition == null || u.condition.condition == Status.Condition.WET)
                u.setStatus(Status.Condition.WET, 1);
            else u.setStatus(Status.Condition.NONE);
        }
    }
}
