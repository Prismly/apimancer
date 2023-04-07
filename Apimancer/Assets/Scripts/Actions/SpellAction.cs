using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAction : Action
{
    public SpellAction(Unit from, uint range, uint cost)
        : base(ActionType.SPELL, from, range, cost) { }

    public enum SpellType { 
        HONEY_TRAP,
        HONEY_BLAST,
        TELEPORT
    }
}
