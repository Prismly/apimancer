using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ant : Unit
{
    private Wizard commander;

    public void OnSpawn()
    {
        zOffset = 0.28f;
        myShadow.transform.position = new Vector3(myShadow.transform.position.x, myShadow.transform.position.y, zOffset - 0.01f);
    }

    public Wizard GetCommander()
    {
        return commander;
    }

    public void SetCommander(Wizard w)
    {
        commander = w;
    }
}
