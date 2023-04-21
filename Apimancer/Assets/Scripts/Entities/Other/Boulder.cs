using System.Collections;

public class Boulder : Other
{
    private int maxHealth = 99;
    private int health = 99;

    public override IEnumerator DetermineMovement()
    {
        // do movement
        return null;
    }

    public override int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }
}
