using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : Unit
{
    public enum ResourceType {
        FLOWER
    }
    public abstract ResourceType Type { get; set; }

    public static Resource CreateResource(ResourceType resourceType)
    {
        Resource newResource;
        switch (resourceType) {
            case ResourceType.FLOWER:
                newResource = new Flower();
                break;
            default:
                newResource = null;
                break;
        }
        return newResource;
    }

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
