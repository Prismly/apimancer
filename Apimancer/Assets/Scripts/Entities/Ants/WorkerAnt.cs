using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    public static int Cost = 5;
    private int maxHealth = 2;
    private int health = 2;
    private int attackDamage = 1;
    private int movementSpeed = 4;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.RESOURCE, Unit.Faction.BEE };

    public override IEnumerator DetermineMovement()
    {
        Tuple<Unit, int, List<Cell>> target = DetermineTarget();
        if (target != null)
        {
            yield return StartCoroutine(this.MoveAlongPathByAmount(target.Item3, MovementSpeed));
            if (target.Item2 <= MovementSpeed)
            {
                AttackTarget(AttackDamage, target.Item1);
                yield break;
            }
        }
        RelinquishControl();
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
