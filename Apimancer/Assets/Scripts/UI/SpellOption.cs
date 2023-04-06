using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SpellOption : MenuOption
{
    
    private SpellAction action;

    private void Start()
    {
        //sumAct = new SummonAction(GameManager.Instance.CurrentWizard, Unit.UnitType.BEE_WORKER, 1, 0);
    }

    public void SetSpellAction(SpellAction action)
    {
        this.action = action;
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
        SetOptionString("SELECT SUCCESSFUL");
        GameManager.Instance.CurrentAction = action;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
    }
}
