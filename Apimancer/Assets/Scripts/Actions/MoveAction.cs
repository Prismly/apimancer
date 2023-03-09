using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    [SerializeField]
    Vector2Int target;
    public MoveAction(ref Unit unit, Vector2Int loc)
        : base(ref unit)
    => target = loc;

    public override void Execute()
    {
        Cell c = CellManager.Instance.GetCell(target);
        unit.MoveToCell(c);
    }
}
