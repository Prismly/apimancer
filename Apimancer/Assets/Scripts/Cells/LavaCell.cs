public class LavaCell : Cell
{
    private CellType type = CellType.LAVA;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public override void OnEnter() {
        Unit u = Occupant as Unit;
        if (u != null) {
            if (u.condition == null || u.condition.condition == Status.Condition.BURNED)
                u.setStatus(Status.Condition.BURNED, 1);
            else u.setStatus(Status.Condition.NONE);
        }
    }
}
