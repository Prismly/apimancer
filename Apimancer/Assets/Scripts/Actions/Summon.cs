using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Action
{
    [SerializeField]
    public Unit summoned;

    [SerializeField]
    public Vector2Int location;

    [SerializeField]
    public short type;

    public Summon(ref Unit summoner, ref Unit summoned, Vector2Int loc, short type, uint range, uint cost)
        : base(ref summoner, range, cost)
    {
        this.summoned = summoned;
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
        w.Summon(c, w.UnitFaction, type);
    }
}
