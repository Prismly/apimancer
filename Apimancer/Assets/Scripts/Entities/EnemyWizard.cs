
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : Wizard
{
    private int maxHealth = 15;
    private int health = 15;
    private int attackDamage = 1;
    private int movementSpeed = 2;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>();

    private List<Action> summons = new List<Action>();
    private List<Action> spells = new List<Action>();

    private void Awake()
    {
        summons.Add(new SummonAction(this, UnitType.ANT_WORKER, 1, 5));
        summons.Add(new SummonAction(this, UnitType.ANT_ARMY, 1, 8));
        summons.Add(new SummonAction(this, UnitType.ANT_FIRE, 1, 10));
    }

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
        PlaySound(Sounds.Warcry);

        MoveUnits();

        // Randomly decide in which direction to move.

        // Cell occupying = CellManager.Instance.GetCell(loc);
        // bool[] validNeighbors = { false, false, false, false, false, false };
        // int validNeighborCount = 0;
        // for (int i = 0; i < 6; i++)
        // {
        //     if (occupying.GetAdjacent(i) != null)
        //     {
        //         validNeighborCount++;
        //         validNeighbors[i] = true;
        //     }
        // }

        // int randMove = Random.Range(0, validNeighborCount);

        // if (validNeighborCount == 0)
        // {
        //     return;
        // }

        // for (int i = 0; i < 6; i++)
        // {
        //     if (validNeighbors[i])
        //     {
        //         if (randMove <= 0)
        //         {
        //             MoveToCell(occupying.GetAdjacent(i));
        //             GameManager.Instance.NextTurn();
        //             break;
        //         }
        //         else
        //         {
        //             randMove--;
        //         }
        //     }
        // }

        // List<Cell> adjacentCells = this.GetCell().GetAdjacentList();
        // summons[0].Execute(adjacentCells[Random.Range(0, adjacentCells.Count)]);
    }

    public override void MoveUnits()
    {
        _currentUnitIndex = -2;
        MoveNextUnit();
    }

    private void CastSpells()
    {
        List<Cell> summonRange = GetCell().GetAdjacentList();
        int summonIndex = Random.Range(0, summons.Count);
        int cellIndex = Random.Range(0, summonRange.Count);

        // Select summon based on cost here

        // Action castSummon = summons[summonIndex];
        Action castSummon = mana > summons[summonIndex].cost ? summons[summonIndex] : null;
        Cell castCell = null;

        // Select cell based on validity here
        for (int i = 0; i < summonRange.Count; i++)
        {
            Cell cell = summonRange[cellIndex];
            if (!cell.IsOccupied)
            {
                castCell = cell;
                break;
            }
        }
        
        if (castSummon != null && castCell != null)
        {
            castSummon.Execute(castCell);
        }

        MoveNextUnit();
    }

    public override void MoveNextUnit()
    {
        _currentUnitIndex++;
        if (_currentUnitIndex < 0)
        {
            CastSpells();
            return;
        }
        if (_currentUnitIndex >= Units.Count)
        {
            GameManager.Instance.NextTurn();
            return;
        }
        StartCoroutine(Units[_currentUnitIndex].DetermineMovement());
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

    public override List<Unit.Faction> TargetPriorities
    {
        get { return targetPriorities; }
        set { targetPriorities = value; }
    }
}
