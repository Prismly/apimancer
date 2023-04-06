using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    [SerializeField]
    public int cost { get; set; }

    [SerializeField]
    public Unit unit { get; set; }

    [SerializeField]
    public uint range { get; set; }

    public Action(Unit unit, uint range, int cost)
    {
        this.unit = unit;
        this.cost = cost;
        this.range = range;
    }

    protected bool Validate(Cell cell)
    {
        if (Vector2Int.Distance(unit.loc, cell.Location) <= range)
        {
            Wizard w = (Wizard)unit;
            if (w.getMana() >= cost)
            {
                w.addMana(-cost);
                return true;
            }
        }

        return false;
    }

    public abstract bool Execute(Cell cell);
}
