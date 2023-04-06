using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBox : MonoBehaviour
{
    private List<MenuOption> options = new List<MenuOption>();

    private static int optionFontSize = 8;

    [SerializeField] private GameObject spellsOptPref;
    [SerializeField] private GameObject summonOptPref;

    public void AddSpellsOption(string optionText, Wizard owner, SpellAction.SpellType type, uint range, uint cost)
    {
        SpellAction spellAction = null;
        switch (type) {
            case SpellAction.SpellType.HONEY_TRAP:
                spellAction = new HoneyTrap(owner, range, cost);
                break;
            case SpellAction.SpellType.HONEY_BLAST:
                spellAction = new HoneyBlast(owner, range, cost);
                break;
            case SpellAction.SpellType.TELEPORT:
                spellAction = new Teleport(owner, range, cost);
                break;
        }
        GameObject newOptObj = Instantiate(spellsOptPref);

        SpellOption newOpt = newOptObj.GetComponent<SpellOption>();
        newOpt.SetSpellAction(spellAction);
        options.Add(newOpt);
        newOpt.SetParentBox(this);
        newOpt.SetOptionString(optionText);

        RectTransform thisBoxRect = GetComponent<RectTransform>();
        thisBoxRect.sizeDelta = new Vector2(thisBoxRect.sizeDelta.x, optionFontSize * (options.Count + 1) + 2);

        RectTransform newOptRect = newOptObj.GetComponent<RectTransform>();
        RectTransform optPrefRect = spellsOptPref.GetComponent<RectTransform>();
        //newOptRect.offsetMin = new Vector2(optPrefRect.offsetMin.x, optPrefRect.offsetMin.y);
        //newOptRect.offsetMax = new Vector2(optPrefRect.offsetMax.x, optPrefRect.offsetMax.y + (optionFontSize * (options.Count - 1)));
        newOptRect.sizeDelta = new Vector2(newOptRect.sizeDelta.x, optionFontSize);
        newOptRect.anchoredPosition = new Vector2(newOptRect.anchoredPosition.x, -optionFontSize * options.Count);
        newOptRect.offsetMin = new Vector2(4, newOptRect.offsetMin.y);
        newOptRect.offsetMax = new Vector2(-4, newOptRect.offsetMax.y);
        newOptRect.localScale = optPrefRect.localScale;
    }

    public void AddSummonOption(string optionText, Wizard owner, Unit.UnitType summon, uint range, uint cost)
    {
        GameObject newOptObj = Instantiate(summonOptPref);

        SummonOption newOpt = newOptObj.GetComponent<SummonOption>();
        newOpt.SetSummonAction(new SummonAction(owner, summon, range, cost));
        options.Add(newOpt);
        newOpt.SetParentBox(this);
        newOpt.SetOptionString(optionText);

        RectTransform thisBoxRect = GetComponent<RectTransform>();
        thisBoxRect.sizeDelta = new Vector2(thisBoxRect.sizeDelta.x, optionFontSize * (options.Count + 1) + 2);

        RectTransform newOptRect = newOptObj.GetComponent<RectTransform>();
        RectTransform optPrefRect = spellsOptPref.GetComponent<RectTransform>();
        //newOptRect.offsetMin = new Vector2(optPrefRect.offsetMin.x, optPrefRect.offsetMin.y);
        //newOptRect.offsetMax = new Vector2(optPrefRect.offsetMax.x, optPrefRect.offsetMax.y + (optionFontSize * (options.Count - 1)));
        newOptRect.sizeDelta = new Vector2(newOptRect.sizeDelta.x, optionFontSize);
        newOptRect.anchoredPosition = new Vector2(newOptRect.anchoredPosition.x, -optionFontSize * options.Count);
        newOptRect.offsetMin = new Vector2(4, newOptRect.offsetMin.y);
        newOptRect.offsetMax = new Vector2(-4, newOptRect.offsetMax.y);
        newOptRect.localScale = optPrefRect.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
