using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    public static int Cost = 5;
    public static string Name = "Worker Ant";
    public static int maxHealth = 2;
    private int health = 2;
    public static int attackDamage = 1;
    public static int attackRange = 1;
    public static int movementSpeed = 4;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.RESOURCE, Unit.Faction.BEE };

    private void Awake()
    {
        unitName = "Worker Ant";
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
