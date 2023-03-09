using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Resource
{
    private float maxHealth = 5.0f;
    private float health = 5.0f;
    private float attackDamage = 0.0f;
    private float movementSpeed = 0.0f;
    private ResourceType resourceType = ResourceType.FLOWER;

    public override void DetermineAction()
    {
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

    public override ResourceType Type 
    {
        get { return resourceType; }
        set { resourceType = value; }
    }

    public override Cell FindMovementTarget(List<Entity> entities) {
        return null;
    }
}
