using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBee : Bee
{
    public static float Cost = 5.0f;

    private float maxHealth = 2.0f;
    private float health = 2.0f;
    private float attackDamage = 2.0f;
    private float movementSpeed = 5.0f;

    public override MoveAction DetermineMovement()
    {
        
        return null;
    }

    public override Action DetermineAction()
    {
        // do action
        return null;
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

    public override Cell FindMovementTarget(List<Entity> entities) {
        return null;
    }
}
