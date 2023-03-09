using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bee : Unit
{
    public enum BeeType
    {
        WORKER,
        MINING,
        BUMBLE
    }
    public abstract BeeType Type { get; set; }
    public abstract float Cost { get; set; }

    public static Bee CreateBee(BeeType beeType)
    {
        Bee newBee;
        switch (beeType) {
            case BeeType.WORKER:
                newBee = new WorkerBee();
                break;
            case BeeType.MINING:
                newBee = new MiningBee();
                break;
            case BeeType.BUMBLE:
                newBee = new BumbleBee();
                break;
            default:
                newBee = null;
                break;
        }
        return newBee;
    }

    private Wizard commander;
    public Wizard GetCommander() {
        return commander;
    }
    public void SetCommander(Wizard w) {
        commander = w;
    }
}
