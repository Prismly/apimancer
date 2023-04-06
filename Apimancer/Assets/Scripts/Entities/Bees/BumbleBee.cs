using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBee : Bee
{
    public static float Cost = 8.0f;

    private int maxHealth = 5;
    private int health = 5;
    private int attackDamage = 2;
    private int movementSpeed = 4;

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

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        return null;
    }
}
