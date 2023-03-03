using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : Unit
{
    public enum ResourceType {
        FLOWER
    }
    public abstract ResourceType Type { get; set; }

    private Faction faction = Faction.RESOURCE;
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
