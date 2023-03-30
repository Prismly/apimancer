using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAction : Action
{
    public SpellAction(Unit from, uint range, uint cost)
        : base(from, range, cost)
    {
    }
}
