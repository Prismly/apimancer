using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnt : Ant
{
    public static float Cost = 10.0f;

    private float maxHealth = 3.0f;
    private float health = 3.0f;
    private float attackDamage = 3.0f;
    private float movementSpeed = 5.0f;

    public override void DetermineAction() {
        // do action
    }

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

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
