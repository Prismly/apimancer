using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SpellOption : MenuOption
{
    
    private SpellAction spellAct;

    private void Start()
    {
        //sumAct = new SummonAction(GameManager.Instance.CurrentWizard, Unit.UnitType.BEE_WORKER, 1, 0);
    }

    public void SetSpellAction(SpellAction action)
    {
        this.spellAct = action;
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
        // Do whatever the Spell does here
        if (GameManager.Instance.CurrentAction != spellAct)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            GameManager.Instance.SetCurrentAction(spellAct);
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 0f);
            GameManager.Instance.SetCurrentAction(null);
        }
    }
}
