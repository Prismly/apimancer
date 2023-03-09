using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action
{
    [SerializeField]
    Vector2Int target;
    public Move(ref Unit unit, Vector2Int loc)
        : base(ref unit)
    => target = loc;

    public override void Execute()
    {
        Cell c = CellManager.Instance.GetCell(target);
        unit.MoveToCell(c);
    }
}
