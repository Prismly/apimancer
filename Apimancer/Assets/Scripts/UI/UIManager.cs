using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas;
    [SerializeField] private GameObject targetWorldCanvas;

    [SerializeField] private GameObject menuBoxPref;
    [SerializeField] private GameObject healthBox;
    
    private GameObject summonMenu;
    private GameObject spellsMenu;
    //private GameObject healthBox;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        _instance = this;

        // -- SPELLS MENU --
        spellsMenu = Instantiate(menuBoxPref);
        spellsMenu.transform.SetParent(targetCanvas.transform);
        RectTransform spellsMenuRect = spellsMenu.GetComponent<RectTransform>();
        RectTransform menuPrefRect = menuBoxPref.GetComponent<RectTransform>();
        spellsMenuRect.localPosition = new Vector3(0, -120, 0);
        spellsMenuRect.localScale = menuPrefRect.localScale;
        Image spellsMenuImg = spellsMenu.GetComponent<Image>();
        spellsMenuImg.color = new Color(177 / 255f, 142 / 255f, 200 / 255f);
        MenuBox spellsMenuBox = spellsMenu.GetComponent<MenuBox>();
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_BLK, "[X]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_YLW, "[O]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_ATK, "[A]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_MOV, "[M]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_RNG, "[R]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_TRG, "[T]"));
        spellsMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.HP_SHLD, "[S]"));
        // We'd get the player's Spells here
        string[] spellOptions = { "[X]Fancy Hat [M]3 [S]2", "[X]Close Cuts [A]2 [T]2", "[X]Extra Lift [A]3 [R]5" };
        for (int i = 0; i < spellOptions.Length; i++)
        {
            spellsMenuBox.AddSpellsOption(spellOptions[i]);
        }
        spellsMenu.SetActive(false);

        // -- SUMMON MENU --
        summonMenu = Instantiate(menuBoxPref);
        summonMenu.transform.SetParent(targetCanvas.transform);
        RectTransform summonMenuRect = summonMenu.GetComponent<RectTransform>();
        summonMenuRect.localPosition = new Vector3(0, -120, 0);
        summonMenuRect.localScale = menuPrefRect.localScale;
        Image summonMenuImg = summonMenu.GetComponent<Image>();
        summonMenuImg.color = new Color(200 / 255f, 144 / 255f, 143 / 255f);
        MenuBox summonMenuBox = summonMenu.GetComponent<MenuBox>();
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_BLK, "[X]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.DOT_YLW, "[O]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_ATK, "[A]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_MOV, "[M]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_RNG, "[R]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.ACT_TRG, "[T]"));
        summonMenuBox.AddOptionAddon(new OptionAddon(OptionAddon.AddonID.HP_SHLD, "[S]"));
        // We'd get the player's Summons here
        string[] summonOptions = { "[X]Worker Bee [R]1", "[X]Carpenter Bee (Charlie) [R]1", "[X]Queen Bee [R]1" };
        for (int i = 0; i < summonOptions.Length; i++)
        {
            summonMenuBox.AddSummonOption(summonOptions[i]);
        }
        summonMenu.SetActive(false);
    }

    public void ToggleSpellsMenu()
    {
        summonMenu.SetActive(false);
        spellsMenu.SetActive(!spellsMenu.activeInHierarchy);
    }

    public void ToggleSummonMenu()
    {
        spellsMenu.SetActive(false);
        summonMenu.SetActive(!summonMenu.activeInHierarchy);
    }

    public void EndPlayerTurn()
    {
        GameManager.Instance.NextTurn();
    }

    public void ShowHealthBox(Unit target)
    {
        // -- HEALTH BOX --
        //if (healthBox == null)
        //{
        //    healthBox = Instantiate(healthBoxPref);
        //}

        //healthBox.transform.SetParent(targetCanvas.transform);
        RectTransform healthBoxRect = healthBox.GetComponent<RectTransform>();
        //RectTransform healthBoxPrefRect = healthBoxPref.GetComponent<RectTransform>();
        healthBoxRect.transform.position = Camera.main.WorldToScreenPoint(target.transform.position + new Vector3(0, 0, -1)) / targetCanvas.GetComponent<CanvasScaler>().scaleFactor;
        //healthBoxRect.localScale = healthBoxPref.localScale;
        //Image healthBoxImg = healthBox.GetComponent<Image>();
        //healthBoxImg.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        TextMeshProUGUI healthBoxText = healthBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        healthBoxText.text = "<sprite=6> " + target.Health + "/" + target.MaxHealth;
        healthBox.SetActive(true);
    }

    public void HideHealthBox()
    {
        if (healthBox != null)
        {
            healthBox.SetActive(false);
        }

    }
}
