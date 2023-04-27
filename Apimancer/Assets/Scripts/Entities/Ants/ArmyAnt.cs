using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAnt : Ant
{
    public static int Cost = 8;
    public static string Name = "Army Ant";
    public static int maxHealth = 4;
    private int health = 4;
    public static int attackDamage = 2;
    public static int attackRange = 3;
    public static int movementSpeed = 3;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.BEE };

    private void Awake()
    {
        unitName = "Army Ant";
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
