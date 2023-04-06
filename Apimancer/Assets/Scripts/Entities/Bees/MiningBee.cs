using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBee : Bee
{
    public static float Cost = 10.0f;

    private float maxHealth = 3.0f;
    private float health = 3.0f;
    private float attackDamage = 4.0f;
    private float movementSpeed = 5.0f;

    public override Action DetermineAction()
    {
        return null;
    }

    public override IEnumerator DetermineMovement()
    {
        Dictionary<Unit.Faction, List<Unit>> dUnits = GameManager.Instance.Units;
        List<Unit> lUnits = null;
        Tuple<Unit, short, List<Cell>> target = null;
        Tuple<Unit, short, List<Cell>> priorityTarget = null;
        if (dUnits.ContainsKey(Unit.Faction.RESOURCE))
        {
            lUnits = dUnits[Unit.Faction.RESOURCE];
            priorityTarget = this.FindClosestTarget(lUnits);
            if (priorityTarget.Item2 < movementSpeed)
            {
                target = priorityTarget;
            }
        }
        if (target == null)
        {
            if (dUnits.ContainsKey(Unit.Faction.ANT))
            {
                lUnits = dUnits[Unit.Faction.ANT];
                target = this.FindClosestTarget(lUnits);
            }
        }
        if (target == null) target = priorityTarget;
        if (target != null)
        {
            yield return StartCoroutine(this.MoveAlongPathByAmount(target.Item3, MovementSpeed));
            if (target.Item2 <= MovementSpeed + 1)
            {
                animator.SetInteger("state", (int)BeeAnimState.STING);
                Unit.DamageTarget(AttackDamage, target.Item1);
                yield return new WaitForSeconds(1f);
            }
            animator.SetInteger("state", (int)BeeAnimState.IDLE);
        }
        GameManager.Instance.NotifyNextUnit();
    }

    public override float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override float Health
    {
        get { return health; }
        set { health = value; }
    }

    public override float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public override float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
