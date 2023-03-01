using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas;
    [SerializeField] private GameObject menuBoxPref;
    
    private GameObject summonMenu;
    private GameObject spellsMenu;

    private void Start()
    {
        spellsMenu = Instantiate(menuBoxPref);
        spellsMenu.transform.SetParent(targetCanvas.transform);
        RectTransform spellsMenuRect = spellsMenu.GetComponent<RectTransform>();
        RectTransform menuPrefRect = menuBoxPref.GetComponent<RectTransform>();
        spellsMenuRect.localPosition = new Vector3(50, 50, 0);
        spellsMenuRect.localScale = menuPrefRect.localScale;
        MenuBox spellsMenuBox = spellsMenu.GetComponent<MenuBox>();
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_BLK, "[X]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_YLW, "[O]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_ATK, "[A]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_MOV, "[M]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_RNG, "[R]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_TRG, "[T]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.HP_SHLD, "[S]"));
        // We'd get the player's Spells here
        string[] testOptions = { "[X]Fancy Hat [M]3 [S]2", "[X]Close Cuts [A]2 [T]2", "[X]Extra Lift [A]3 [R]5" };
        for (int i = 0; i < testOptions.Length; i++)
        {
            spellsMenuBox.AddOption(testOptions[i]);
        }
    }

    public void OpenSpellsMenu()
    {
        
    }
}
