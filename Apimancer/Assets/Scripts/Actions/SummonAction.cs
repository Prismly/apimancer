using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : Action
{
    public Unit.UnitType type;

    public SummonAction(Unit summoner, Unit.UnitType type, uint range, int cost)
        : base(ActionType.SUMMON, summoner, range, cost)
    {
        this.type = type;
    }

    public override bool Validate(Cell cell)
    {
        return !cell.IsOccupied;
    }

    public override bool Execute(Cell cell)
    {
        Wizard w = (Wizard)unit;
        return w.Summon(type, cell, range);
    }
}
