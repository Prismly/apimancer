using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class MenuOption : MonoBehaviour
{
    protected MenuBox parentBox;
    protected string optionString;

    public void SetParentBox(MenuBox parentBox)
    {
        this.parentBox = parentBox;
        transform.SetParent(parentBox.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void SetOptionString(string optionString)
    {
        this.optionString = optionString;
        TextMeshProUGUI textComp = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textComp.text = ToString();
    }

    public override string ToString()
    {
        string result = optionString;

        // Replace any flag sequences with their corresponding sprite string.
        // POTENTIAL OPTIMIZATION: allow menu boxes to choose which icons they're "listening" for, rather than testing all icons
        result = OptionAddon.Mutate(optionString);

        return result;
    }

    public abstract void OnSelect();
}
