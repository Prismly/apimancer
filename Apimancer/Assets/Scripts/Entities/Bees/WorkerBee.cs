using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBee : Bee
{
    public static float Cost = 5.0f;

    private float maxHealth = 2.0f;
    private float health = 2.0f;
    private float attackDamage = 2.0f;
    private float movementSpeed = 5.0f;

    public override MoveAction DetermineMovement()
    {
<<<<<<< Updated upstream
        
=======
        this.DetermineMovement();
>>>>>>> Stashed changes
        return null;
    }

    public override Action DetermineAction()
    {
<<<<<<< Updated upstream
        // do action
=======
        Dictionary<Unit.Faction, List<Unit>> dUnits = GameManager.Instance.Units;
        List<Unit> lUnits = null;
        float remainingMovement = movementSpeed;
        Tuple<Unit, short, List<Cell>> target = null;
        Tuple<Unit, short, List<Cell>> priorityTarget = null;

        if (dUnits.ContainsKey(Unit.Faction.RESOURCE)) {
            lUnits = dUnits[Unit.Faction.RESOURCE];
            priorityTarget = this.FindClosestTarget(lUnits);
            if (priorityTarget.Item2 < remainingMovement) {
                target = priorityTarget;
            }
        }
        if (target == null) {
            Tuple<Unit, short, List<Cell>> tempTarget = null;
            if (dUnits.ContainsKey(Unit.Faction.ANT)) {
                lUnits = dUnits[Unit.Faction.ANT];
                tempTarget = this.FindClosestTarget(lUnits);
                if (tempTarget.Item2 < remainingMovement) {
                    target = tempTarget;
                }
            }
        }
        
        if (target != null) {
            StartCoroutine(this.MoveAlongPathByAmount(target.Item3, this.movementSpeed));
        }

>>>>>>> Stashed changes
        return null;
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

    public override Cell FindMovementTarget(List<Entity> entities) {
        return null;
    }
}
