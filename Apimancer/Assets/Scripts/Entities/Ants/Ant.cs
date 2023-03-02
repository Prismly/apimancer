using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ant : Unit
{
    public enum AntType {
        WORKER,
        FIRE,
        ARMY
    }
    public abstract AntType Type { get; set; }
    public abstract float Cost { get; set; }

    private Faction faction = Faction.ANT;
    public override Faction UnitFaction
    {
        get
        {
            return faction;
        }
        set
        {
            faction = value;
        }
    }
}
