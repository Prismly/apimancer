using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonOption : MenuOption
{
    private SummonAction sumAct;

    private void Start()
    {
        //sumAct = new SummonAction(GameManager.Instance.CurrentWizard, Unit.UnitType.BEE_WORKER, 1, 0);
    }

    public void SetSummonAction(SummonAction sumAct)
    {
        this.sumAct = sumAct;
    }

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
        GameManager.Instance.CurrentAction = sumAct;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
    }
}
