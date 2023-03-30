using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    public enum Condition {
        HONEYED,
        BURNED,
        WET
    }

    protected Unit unit;
    protected short duration;
    protected Condition condition;

    public abstract void doStatus();
}
