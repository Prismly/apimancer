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
            u.setStatus(Status.Condition.WET, 1);
        }
    }

    public override void OnEndTurn()
    {
        Unit u = Occupant as Unit;
        if (u != null)
        {
            u.setStatus(Status.Condition.WET, 1);
        }
    }
}
