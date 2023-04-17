using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : Wizard
{
    private int maxHealth = 15;
    private int health = 15;
    private int attackDamage = 1;
    private int movementSpeed = 2;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction> { Unit.Faction.BEE };

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
        Dictionary<Unit.Faction, List<Unit>> dUnits = GameManager.Instance.Units;
        List<Unit> lUnits = null;
        Tuple<Unit, int, List<Cell>> target = null;
        HashSet<Cell> possibleMoves = new HashSet<Cell>();
        List<Cell> adjList = CellManager.Instance.GetCell(loc).GetAdjacentList();
        foreach (Cell c in adjList) {
            if (!c.IsOccupied)
                possibleMoves.Add(c);
        }

        List<HashSet<Cell>> adjListSet = new List<HashSet<Cell>>();
        int i = 0;
        foreach (Cell c in possibleMoves) {
            adjListSet.Add(new HashSet<Cell>());
            adjList = c.GetAdjacentList();
            foreach (Cell d in adjList) {
                if (!d.IsOccupied)
                    adjListSet[i].Add(d);
            }
            i++;
        }

        foreach (HashSet<Cell> h in adjListSet) {
            possibleMoves.UnionWith(h);
        }

        i = TargetPriorities.Count;
        Unit.Faction f = TargetPriorities[0];
        if (dUnits.ContainsKey(f)) {
            lUnits = dUnits[f];
            foreach (Cell c in possibleMoves) {
                Tuple<Unit, int, List<Cell>> tempTarget = FindClosestTarget(lUnits, c);
                if (target == null || tempTarget.Item2 > target.Item2)
                    target = tempTarget;
            }
        }

        if (target != null) {
            yield return StartCoroutine(MoveToCellCoroutine(target.Item3[target.Item2 - 1]));
        }

        RelinquishControl();
    }

    public override void BeginTurn()
    {
        IsTurn = true;
        PlaySound(Sounds.Warcry);

        MoveUnits();
    }

    public override void MoveUnits()
    {
        _currentUnitIndex = -3;
        MoveNextUnit();
    }

    private void CastSpells()
    {
        List<Cell> summonRange = GetCell().GetAdjacentList();
        int summonIndex = UnityEngine.Random.Range(0, summons.Count);
        int cellIndex = UnityEngine.Random.Range(0, summonRange.Count);

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
        if (_currentUnitIndex < -1)
        {
            StartCoroutine(DetermineMovement());
            return;
        }
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
        set { 
            health = value;
            if (health <= 0)
                GameManager.Instance.GameOver(true);
        }
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
