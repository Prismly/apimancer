public class Wet : Status
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

    public Wet (Unit unit, int duration) {
        this.unit = unit;
        this.duration = duration;
        condition = Condition.WET;
    }

    public override void doStatus() {
        if (unit.Type == Unit.UnitType.ANT_FIRE)
            unit.ReceiveDamage(1);
        UpdateStatus();
    }
}
