public abstract class Status
{
    public enum Condition {
        NONE,
        HONEYED,
        BURNED,
        WET
    }

    public abstract Unit unit { get; set; }
    public abstract int duration { get; set; }
    public abstract Condition condition { get; set; }

    public abstract void doStatus();

    public void UpdateStatus() {
        duration--;
        if (duration <= 0)
            unit.setStatus(Condition.NONE, 0);
    }
}
