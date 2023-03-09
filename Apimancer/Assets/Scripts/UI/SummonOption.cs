using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOption : MenuOption
{
    private Cell summonLocation { get; set; }
    private Unit.Faction factionType { get; set; }
    private short unitType { get; set; }

    public void MouseEnter()
    {
        Debug.Log("MouseEnter!");
        SetOptionString(optionString.Replace("[X]", "[O]"));
    }

    public void MouseExit()
    {
        Debug.Log("MouseExit!");
        SetOptionString(optionString.Replace("[O]", "[X]"));
    }

    public override void OnSelect()
    {
        // Do whatever the Summon does here
        //GameManager.Instance.CurrentWizard.Summon
    }
}
