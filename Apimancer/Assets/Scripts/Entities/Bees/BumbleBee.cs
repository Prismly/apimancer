using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBee : Bee
{
    private float maxHealth = 5.0f;
    private float health = 5.0f;
    private float attackDamage = 2.0f;
    private float movementSpeed = 4.0f;
    private float cost = 8.0f;
    private BeeType beeType = BeeType.BUMBLE;

    public override float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override float Health
    {
        get { return health; }
        set { health = value; }
    }

    public override float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public override float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override float Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public override BeeType Type
    {
        get { return beeType; }
        set { beeType = value; }
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
