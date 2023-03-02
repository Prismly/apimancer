using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Spell : MenuOption
{
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
        SetOptionString("SELECT SUCCESSFUL");
    }
}
