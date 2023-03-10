using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWizard : Wizard
{
    private float maxHealth = 15.0f;
    private float health = 15.0f;
    private float attackDamage = 1.0f;
    private float movementSpeed = 2.0f;

    public override void BeginTurn()
    {
        IsTurn = true;
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        throw new System.NotImplementedException();
    }

    public override MoveAction DetermineMovement()
    {
        // do nothing
        return null;
    }

    public override Action DetermineAction()
    {
        // do nothing
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
}
