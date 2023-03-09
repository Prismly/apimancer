using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : Action
{

    [SerializeField]
    public Vector2Int location;

    [SerializeField]
    public Unit.UnitType type;

    public SummonAction(ref Unit summoner, Unit.UnitType type, Vector2Int loc, uint range, uint cost)
        : base(ref summoner, range, cost)
    {
        location = loc;
        this.type = type;
    }

    public override void Execute()
    {
        Wizard w = (Wizard)unit;
        Cell c = CellManager.Instance.GetCell(location);
        List<Cell> path = Entity.PathFind(w, c);
        if (path.Count > range)
            return;
        w.Summon(Unit.UnitType.BEE_WORKER, c);
    }
}
