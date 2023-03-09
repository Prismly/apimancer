using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAction : Action
{
    [SerializeField]
    public Unit target { get; set; }
    public SpellAction(ref Unit from, ref Unit to, uint range, uint cost)
        : base(ref from, range, cost)
    => target = to;
}
