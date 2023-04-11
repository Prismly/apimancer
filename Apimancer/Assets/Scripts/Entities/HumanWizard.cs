using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWizard : Wizard
{
    private int maxHealth = 15;
    private int health = 15;
    private int attackDamage = 1;
    private int movementSpeed = 2;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>();

    public override void BeginTurn()
    {
        IsTurn = true;
        GameManager.Instance.SetCurrentAction(new MoveAction(this));
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator DetermineMovement()
    {
        // do nothing
        return null;
    }

    public override Action DetermineAction()
    {
        // do nothing
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

    public override int AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public override int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override List<Unit.Faction> TargetPriorities
    {
        get { return targetPriorities; }
        set { targetPriorities = value; }
    }
}
