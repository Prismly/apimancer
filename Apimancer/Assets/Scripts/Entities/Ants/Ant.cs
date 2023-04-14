using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ant : Unit
{
    private Wizard commander;

    public Wizard GetCommander()
    {
        return commander;
    }
    public void SetCommander(Wizard w)
    {
        commander = w;
    }
}
