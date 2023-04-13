using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Other : Unit
{
    public static Other CreateOther(Unit.UnitType type)
    {
        Other newOther;
        switch (type) {
            case Unit.UnitType.OTHER_BOULDER:
                newOther = new Boulder();
                break;
            default:
                newOther = null;
                break;
        }
        return newOther;
    }
}
