using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wizard : Unit
{
    [SerializeField] protected int mana = 30;
    private int maxMana = 40;

    public Color Color;
    public bool IsTurn {get; protected set;}
    //[SerializeField] int _summonRange;
    public List<Unit> Units = new List<Unit>();
    protected int _currentUnitIndex;

    public abstract void BeginTurn();
    public virtual void MoveUnits()
    {
        _currentUnitIndex = -1;
        MoveNextUnit();
    }

    public virtual void MoveNextUnit()
    {
        _currentUnitIndex++;
        if (_currentUnitIndex >= Units.Count)
        {
            GameManager.Instance.NextTurn();
            return;
        }
        StartCoroutine(Units[_currentUnitIndex].DetermineMovement());
    }

    public bool Summon(Unit.UnitType type, Cell cell, uint range)
    {
        if (cell.IsOccupied || cell.Type == CellType.BOULDER)
            return false;

        int cost = GameManager.Instance.GetUnitCost(type);

        if (!SpendMana(cost))
        {
            return false;
        }

        List<Cell> path = Entity.PathFind(this, cell);

        if (path == null || path.Count > range + 1)
            return false;
        
        Unit unit = GameManager.Instance.SummonUnit(type, cell);

        unit.UnitFaction = this.UnitFaction;
        unit.setLocation(cell);
        Units.Add(unit);

        return true;
    }

    public int GetMana()
    {
        return mana;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public void SetMana(int m) {
        mana = (m > maxMana) ? 
               (maxMana) :
               (m);
    }

    // Attempts to reduce 'mana' by the value of 'cost'. If this would bring 'mana' below 0, returns false and does not decrement. If successful, returns true.
    public bool SpendMana(int cost)
    {
        if (cost < 0)
        {
            Debug.LogError("Oops! You tried to spend negative mana. Classic blunder");
            return false;
        }

        if (cost > mana)
        {
            // Insufficient mana
            return false;
        }
        else
        {
            mana -= cost;
            return true;
        }
    }

    public void AddMana(int deposit) {
        if (deposit < 0)
        {
            Debug.LogError("Oops! You tried to add negative mana. Classic blunder");
            return;
        }

        int addedVal = mana + deposit;
        // Assign to 'mana', cap value at 'maxMana'
        mana = addedVal < maxMana ? addedVal : maxMana;
    }
}
