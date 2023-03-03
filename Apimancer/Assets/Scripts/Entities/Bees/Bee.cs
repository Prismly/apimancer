using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bee : Unit
{
    public enum BeeType {
        WORKER,
        MINING,
        BUMBLE
    }
    public abstract BeeType Type { get; set; }
    public abstract float Cost { get; set; }

    private Faction faction = Faction.BEE;
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
