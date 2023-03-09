using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnt : Ant
{
    private float maxHealth = 3.0f;
    private float health = 3.0f;
    private float attackDamage = 3.0f;
    private float movementSpeed = 5.0f;
    private float cost = 10.0f;
    private AntType antType = AntType.FIRE;

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

    public override float Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public override AntType Type
    {
        get { return antType; }
        set { antType = value; }
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
