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
            u.setStatus(Status.Condition.HONEYED, 1);
        }
    }

    public override void OnEndTurn()
    {
        Unit u = Occupant as Unit;
        if (u != null)
        {
            u.setStatus(Status.Condition.HONEYED, 1);
        }
    }
}
