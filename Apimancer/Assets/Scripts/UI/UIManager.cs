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
        // We'd get the player's Summons here
        Wizard playerWiz = GameManager.Instance.Wizards[0];
        summonMenuBox.AddSummonOption("[X]Worker Bee [NCT]5 [HPA]2 [ATK]2 [MOV]5", playerWiz, Unit.UnitType.BEE_WORKER, 1, 5);
        summonMenuBox.AddSummonOption("[X]Bumble Bee [NCT]8 [HPA]5 [ATK]2 [MOV]3", playerWiz, Unit.UnitType.BEE_BUMBLE, 1, 8);
        summonMenuBox.AddSummonOption("[X]Miner Bee [NCT]10 [HPA]3 [ATK]4 [MOV]5", playerWiz, Unit.UnitType.BEE_MINING, 1, 10);
        summonMenu.SetActive(false);
    }
    
    public void TogglePlayerMove()
    {
        // the move button has been clicked
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
        GameManager.Instance.EndTurn();
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
        healthBoxRect.sizeDelta = healthBoxRect.sizeDelta;
        Image healthBoxImg = healthBox.GetComponent<Image>();
        healthBoxImg.color = new Color(1, 1, 1);
        TextMeshProUGUI healthBoxText = healthBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        healthBoxText.text = "<sprite=6>" + target.Health + "/" + target.MaxHealth;
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
