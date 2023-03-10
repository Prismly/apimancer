using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : Action
{
    public Unit.UnitType type;

    public SummonAction(ref Unit summoner, Unit.UnitType type, uint range, uint cost)
        : base(ref summoner, range, cost)
    {
        this.type = type;
    }

    public override bool Execute(Cell cell)
    {
        Wizard w = (Wizard)unit;
        return w.Summon(type, cell);
    }
}
