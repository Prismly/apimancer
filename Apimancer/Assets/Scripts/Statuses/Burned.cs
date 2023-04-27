public class Burned : Status
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

    public Burned(Unit unit, int duration) {
        this.unit = unit;
        this.duration = duration;
        condition = Condition.BURNED;
    }

    public override void doStatus() {
        if (unit.Type == Unit.UnitType.ANT_FIRE)
            unit.Heal(1);
        else if (unit.Type == Unit.UnitType.ANT_WIZARD) {
            Wizard w = unit as Wizard;
            w.AddMana(1);
        }
        else unit.ReceiveDamage(1);
        UpdateStatus();
    }
}
