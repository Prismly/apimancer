using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bee : Unit
{
    public enum BeeAnimState 
    { 
        IDLE = 0,
        STING,
        DEATH
    };

    private Wizard commander;

    public Wizard GetCommander() {
        return commander;
    }
    public void SetCommander(Wizard w) {
        commander = w;
    }
}
