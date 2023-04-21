using System.Collections;

public class Flower : Resource
{
    private int maxHealth = 6;
    private int health = 6;
    private int movementSpeed = 0;

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

    public override int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }
}
