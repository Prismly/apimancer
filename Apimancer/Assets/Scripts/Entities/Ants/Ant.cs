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

    public static Ant CreateAnt(AntType antType)
    {
        Ant newAnt;
        switch (antType) {
            case AntType.WORKER:
                newAnt = new WorkerAnt();
                break;
            case AntType.FIRE:
                newAnt = new FireAnt();
                break;
            case AntType.ARMY:
                newAnt = new ArmyAnt();
                break;
            default:
                newAnt = null;
                break;
        }
        return newAnt;
    }

    private Wizard commander;
    public Wizard GetCommander()
    {
        return commander;
    }
    public void SetCommander(Wizard w)
    {
        commander = w;
    }

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
