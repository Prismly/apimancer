using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honeyed : Status
{
    public Honeyed(Unit u, short duration) {
        this.unit = u;
        this.duration = duration;
        condition = Status.Condition.HONEYED;
    }

    public override void doStatus() {
        // do the thing
        duration--;
    }
}
