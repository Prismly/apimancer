using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBee : Bee
{
    public static int Cost = 8;
    private int maxHealth = 5;
    private int health = 5;
    private int attackDamage = 2;
    private int attackRange = 1;
    private int movementSpeed = 4;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.ANT };

    private void Awake()
    {
        OnSpawn();
    }

    public override int MaxHealth
    { get { return maxHealth; }
      set { maxHealth = value; } }

    public override int Health
    { get { return health; }
      set { health = value; } }

    public override int AttackDamage
    { get { return attackDamage; }
      set { attackDamage = value; } }

    public override int AttackRange 
    { get { return attackRange; }
      set { attackRange = value; } }

    public override int MovementSpeed
    { get { return movementSpeed; }
      set { movementSpeed = value; } }

    public override List<Unit.Faction> TargetPriorities
    { get { return targetPriorities; }
      set { targetPriorities = value; } }
}
