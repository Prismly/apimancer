using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnt : Ant
{
    public static int Cost = 10;
    private int maxHealth = 4;
    private int health = 4;
    private int attackDamage = 1;
    private int attackRange = 1;
    private int movementSpeed = 5;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.BEE };

    private void Awake()
    {
        unitName = "Fire Ant";
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
