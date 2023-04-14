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

    private void Awake()
    {
        unitName = "Worker Ant";
    }

    public override IEnumerator DetermineMovement()
    {
        Debug.Log("Worker and moving");
        Tuple<Unit, int, List<Cell>> target = DetermineTarget();
        if (target != null)
        {
            yield return StartCoroutine(this.MoveAlongPathByAmount(target.Item3, MovementSpeed));
            if (target.Item2 <= MovementSpeed)
            {
                animator.SetInteger("state", (int)AntAnimState.BITE);
                Unit.DamageTarget(AttackDamage, target.Item1);
                yield return new WaitForSeconds(1f);
            }
            animator.SetInteger("state", (int)AntAnimState.IDLE);
        }
        GameManager.Instance.NotifyNextUnit();
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

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
