using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wet : Status
{
    public Wet (Unit u, short duration) {
        this.unit = u;
        this.duration = duration;
    }

    public override void doStatus() {
        // do the thing
        duration--;
    }
}
