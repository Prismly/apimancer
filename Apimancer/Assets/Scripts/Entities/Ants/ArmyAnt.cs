using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAnt : Ant
{
    public static int Cost = 8;
    private int maxHealth = 4;
    private int health = 4;
    private int attackDamage = 2;
    private int attackRange = 3;
    private int movementSpeed = 3;
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
