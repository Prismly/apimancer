using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Other
{
    private int maxHealth = 99;
    private int health = 99;
    private int attackDamage = 0;
    private int movementSpeed = 0;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>();

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
