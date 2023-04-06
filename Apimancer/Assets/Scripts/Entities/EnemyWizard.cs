using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : Wizard
{
    private int maxHealth = 15;
    private int health = 15;
    private int attackDamage = 1;
    private int movementSpeed = 2;

    public override IEnumerator DetermineMovement()
    {
        // do movement
        return null;
    }

    public override Action DetermineAction()
    {
        // do action
        return null;
    }

    public override void BeginTurn()
    {
        IsTurn = true;

        // Randomly decide in which direction to move.
        Cell occupying = CellManager.Instance.GetCell(loc);
        bool[] validNeighbors = { false, false, false, false, false, false };
        int validNeighborCount = 0;
        for (int i = 0; i < 6; i++)
        {
            if (occupying.GetAdjacent(i) != null)
            {
                validNeighborCount++;
                validNeighbors[i] = true;
            }
        }

        int randMove = Random.Range(0, validNeighborCount);

        if (validNeighborCount == 0)
        {
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            if (validNeighbors[i])
            {
                if (randMove <= 0)
                {
                    MoveToCell(occupying.GetAdjacent(i));
                    GameManager.Instance.NextTurn();
                    break;
                }
                else
                {
                    randMove--;
                }
            }
        }
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
        throw new System.NotImplementedException();
    }
}
