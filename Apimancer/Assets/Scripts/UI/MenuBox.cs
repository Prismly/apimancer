using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBox : MonoBehaviour
{
    private List<MenuOption> options = new List<MenuOption>();
    private List<OptionAddon> optionAddons = new List<OptionAddon>();

    private static int optionFontSize = 8;

    [SerializeField] private GameObject spellsOptPref;
    [SerializeField] private GameObject summonOptPref;

    public void AddSpellsOption(string optionText)
    {
        GameObject newOptObj = Instantiate(spellsOptPref);

        MenuOption newOpt = newOptObj.GetComponent<MenuOption>();
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

    public void AddSummonOption(string optionText)
    {
        GameObject newOptObj = Instantiate(summonOptPref);

        MenuOption newOpt = newOptObj.GetComponent<MenuOption>();
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

    public void AddOptionAddon(OptionAddon newAddon)
    {
        optionAddons.Add(newAddon);
    }

    public List<OptionAddon> GetAddons()
    {
        return optionAddons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
