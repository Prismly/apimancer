using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellAction : Action
{
<<<<<<< Updated upstream
    public SpellAction(Unit from, uint range, uint cost)
=======
    public enum SpellType { 
        HONEY_TRAP,
        HONEY_BLAST,
        TELEPORT
    }

    public SpellAction(Unit from, uint range, short cost)
>>>>>>> Stashed changes
        : base(from, range, cost)
    {
    }
}
