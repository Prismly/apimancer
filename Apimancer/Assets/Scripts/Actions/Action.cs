using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MOVE,
    SUMMON,
    SPELL,
    ATTACK
}

public abstract class Action
{
    [SerializeField]
    public ActionType actionType { get; set; }

    [SerializeField]
    public int cost { get; set; }

    [SerializeField]
    public Unit unit { get; set; }

    [SerializeField]
    public uint range { get; set; }

    public Action(ActionType type, Unit unit, uint range, int cost)
    {
        this.actionType = type;
        this.unit = unit;
        this.cost = cost;
        this.range = range;
    }

    protected virtual bool Validate(Cell cell)
    {
        List<Cell> inRange = unit.GetCell().GetCellsRange(3);
        foreach (Cell c in inRange)
        {
            if (c.Location == cell.Location)
            {
                Wizard w = (Wizard)unit;
                if (w.SpendMana(cost))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public abstract bool Execute(Cell cell);
}
