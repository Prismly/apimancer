using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBee : Bee
{
    public static int Cost = 10;
    private int maxHealth = 3;
    private int health = 3;
    private int attackDamage = 4;
    private int movementSpeed = 5;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.ANT, Unit.Faction.RESOURCE };

    public override IEnumerator DetermineMovement()
    {
        Tuple<Unit, int, List<Cell>> target = DetermineTarget();
        if (target != null)
        {
            yield return StartCoroutine(this.MoveAlongPathByAmount(target.Item3, MovementSpeed));
            if (target.Item2 <= MovementSpeed)
            {
                AttackTarget(AttackDamage, target.Item1);
            }
            else RelinquishControl();
        }
        else RelinquishControl();
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

    public override int MovementSpeed
    { get { return movementSpeed; }
      set { movementSpeed = value; } }

    public override List<Unit.Faction> TargetPriorities
    { get { return targetPriorities; }
      set { targetPriorities = value; } }
}
