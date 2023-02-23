using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract (temporarily concrete) parent class that all actions performable by the player character derive from.
// Manages a list of "selection actions", which the player performs in order to specify where/what to target.
public class ActionCommand
{
    private string actionName;
    private int actionCost;

    private int movementValue; // The hexes of movement allowed before the action takes place.
    private int rangeValue; // The hexes of range away from the target the action allows (within line of sight).
    private int targetValue; // The number of hexes targetted by this action. Could be enemies, allies, resources, or just empty hexes.

    public ActionCommand(string actionName, int actionCost, int movementValue, int rangeValue, int targetValue)
    {
        this.actionName = actionName;
        this.actionCost = actionCost;
        this.movementValue = movementValue;
        this.rangeValue = rangeValue;
        this.targetValue = targetValue;
    }

    public void DoTheThing()
    {
        Debug.Log("I, " + this + ", do hereby decree that I have done the thing!");
    }
}
