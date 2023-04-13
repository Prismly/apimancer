using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wizard : Unit
{
    [SerializeField] protected int mana = 30;
    private int maxMana = 99;

    // private List<ActionCommand> summonList = new List<ActionCommand>();
    // private List<ActionCommand> spellsList = new List<ActionCommand>();

    public Color Color;
    public bool IsTurn {get; protected set;}
    //[SerializeField] int _summonRange;
    public List<Unit> Units = new List<Unit>();
    protected int _currentUnitIndex;

    // private void Start()
    // {
    //     Action Command Constructor is NAME, COST, MOVE, RANGE, TARGS
    //     NOTE: These are temporary! There will be a script for each 
    //     summonList.Add(new ActionCommand("Summon Bee", 1, 0, 3, 1));
    //     summonList.Add(new ActionCommand("Sweeping Summon", 5, 0, 1, 3));
    //     summonList.Add(new ActionCommand("Summon Beast", 10, 0, ));

    //     spellsList.Add(new ActionCommand("Move", 1));
    //     spellsList.Add(new ActionCommand("Harvest", 5));
    //     spellsList.Add(new ActionCommand("Pheromone Lure", 5));
    // }

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
        animator.SetTrigger("Unit Summoned");

        List<Cell> path = Entity.PathFind(this, cell);
        if (path.Count > range + 1)
            return false;
        Unit unit = GameManager.Instance.SummonUnit(type, cell);
        unit.UnitFaction = this.UnitFaction;
        Units.Add(unit);
        unit.setLocation(cell);
        return true;
    }

    public int getMana() {
        return mana;
    }

    public int getMaxMana()
    {
        return maxMana;
    }

    public void setMana(int m) {
        mana = (m > maxMana) ? 
               (maxMana) :
               (m);
    }
    public void addMana(int m) {
        int d = maxMana - mana;
        mana = m > d ? 
               maxMana : 
               m + mana;
    }
}
