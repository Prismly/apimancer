using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAction : Action
{
    public SpellAction(ref Unit from, ref Unit to, uint range, uint cost)
        : base(ref from, range, cost)
    {

    }
}
