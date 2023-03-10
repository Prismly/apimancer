using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    [SerializeField]
    public uint cost { get; set; }

    [SerializeField]
    public Unit unit { get; set; }

    [SerializeField]
    public uint range { get; set; }

    public Action(ref Unit unit, uint range = 0, uint cost = 0)
    {
        this.unit = unit;
        this.cost = cost;
        this.range = range;
    }

    public abstract bool Execute(Cell cell);
}
