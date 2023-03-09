using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : Wizard
{
    public override Faction UnitFaction { get; set; }
    public override float MaxHealth { get; set; }
    public override float Health { get; set; }
    public override float AttackDamage { get; set; }
    public override float MovementSpeed { get; set; }

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
    
    public override Cell FindMovementTarget(List<Entity> entities)
    {
        throw new System.NotImplementedException();
    }
}
