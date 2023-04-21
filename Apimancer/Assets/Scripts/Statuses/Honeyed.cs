public class Honeyed : Status
{
    /******** start fields ********/
    private Unit u;
    private int d;
    private Condition c;

    public override Unit unit
    {
        get { return u; }
        set { u = value; }
    }

    public override int duration
    {
        get { return d; }
        set { d = value; }
    }

    public override Condition condition
    {
        get { return c; }
        set { c = value; }
    }

    /******** end fields ********/

    public Honeyed(Unit unit, int duration) {
        this.unit = unit;
        this.duration = duration;
        condition = Condition.HONEYED;
    }

    public override void doStatus() {
        Unit.UnitType t = unit.Type;
        switch (t) {
            case Unit.UnitType.BEE_WORKER:
            case Unit.UnitType.BEE_BUMBLE:
            case Unit.UnitType.BEE_MINING:
                unit.Heal(1);
                break;
            default:
                break;
        }
        UpdateStatus();
    }
}
