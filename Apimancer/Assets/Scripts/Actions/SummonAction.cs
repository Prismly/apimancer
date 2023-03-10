using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : Action
{
    public Unit.UnitType type;

    public SummonAction(Unit summoner, Unit.UnitType type, uint range, uint cost)
        : base(summoner, range, cost)
    {
        this.type = type;
    }

    public override bool Execute(Cell cell)
    {
        Wizard w = (Wizard)unit;
        return w.Summon(type, cell);
    }
}
