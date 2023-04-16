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
    [SerializeField] public GameObject endTurn;

    [SerializeField] private List<GameObject> disabledOnPause;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private RectTransform bottomMid;
    [SerializeField] private GameObject damageIndic;

    [SerializeField] public GameObject HEAD_HOR;
    [SerializeField] public GameObject HEAD_DIAG;
    [SerializeField] public GameObject BODY_DIAG;
    [SerializeField] public GameObject BODY_HOR;
    [SerializeField] public GameObject BODY_CURVE;

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

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Wizard playerWiz = GameManager.Instance.Wizards[0];

        // -- SPELLS MENU --
        spellsMenu = Instantiate(menuBoxPref);
        spellsMenu.transform.SetParent(targetCanvas.transform);
        RectTransform spellsMenuRect = spellsMenu.GetComponent<RectTransform>();
        RectTransform menuPrefRect = menuBoxPref.GetComponent<RectTransform>();
        spellsMenuRect.localPosition = bottomMid.localPosition + (Vector3.up * bottomMid.sizeDelta.y * 5);
        spellsMenuRect.localScale = menuPrefRect.localScale;
        Image spellsMenuImg = spellsMenu.GetComponent<Image>();
        spellsMenuImg.color = new Color(177 / 255f, 142 / 255f, 200 / 255f);
        MenuBox spellsMenuBox = spellsMenu.GetComponent<MenuBox>();
        // We'd get the player's Spells here
        spellsMenuBox.AddSpellsOption("[X]Honey Trap [NCT]3 [RNG]3", playerWiz, SpellAction.SpellType.HONEY_TRAP, 3, 3);
        spellsMenuBox.AddSpellsOption("[X]Honey Blast [NCT]3 [RNG]3", playerWiz, SpellAction.SpellType.HONEY_BLAST, 3, 3);
        spellsMenuBox.AddSpellsOption("[X]Teleport [NCT]3 [RNG]3", playerWiz, SpellAction.SpellType.TELEPORT, 3, 3);
        spellsMenu.SetActive(false);

        // -- SUMMON MENU --
        summonMenu = Instantiate(menuBoxPref);
        summonMenu.transform.SetParent(targetCanvas.transform);
        RectTransform summonMenuRect = summonMenu.GetComponent<RectTransform>();
        summonMenuRect.localPosition = bottomMid.localPosition + (Vector3.up * bottomMid.sizeDelta.y * 5);
        summonMenuRect.localScale = menuPrefRect.localScale;
        Image summonMenuImg = summonMenu.GetComponent<Image>();
        summonMenuImg.color = new Color(200 / 255f, 144 / 255f, 143 / 255f);
        MenuBox summonMenuBox = summonMenu.GetComponent<MenuBox>();
        // We'd get the player's Summons here
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
        Debug.Log("toggle summon menu");
        spellsMenu.SetActive(false);
        summonMenu.SetActive(!summonMenu.activeInHierarchy);
    }

    public void EndPlayerTurn()
    {
        UIManager.Instance.endTurn.GetComponent<Button>().interactable = false;
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
        //healthBoxRect.transform.position = Camera.main.WorldToScreenPoint(target.transform.position + new Vector3(0, 0, -1)) / targetCanvas.GetComponent<CanvasScaler>().scaleFactor;
        //healthBoxRect.sizeDelta = healthBoxRect.sizeDelta;
        //Image healthBoxImg = healthBox.GetComponent<Image>();
        //healthBoxImg.color = new Color(1, 1, 1);
        TextMeshProUGUI unitNameText = healthBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        unitNameText.text = target.unitName;
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

    public void TogglePause()
    {
        Debug.Log("Here");
        if (GameManager.Instance.gameIsPaused)
        {
            GameManager.Instance.gameIsPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("enabling");
            foreach (GameObject g in disabledOnPause)
            {
                g.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            GameManager.Instance.gameIsPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("disabling");
            foreach (GameObject g in disabledOnPause)
            {
                g.GetComponent<Button>().enabled = false;
            }
        }
    }

    public void SpawnDamageIndicator(int dmgVal, Vector3 worldPos)
    {
        Debug.Log("Spawning DamageIndicator");
        GameObject newDamageIndic = Instantiate(damageIndic);
        newDamageIndic.transform.SetParent(targetCanvas.transform);
        RectTransform indicRect = newDamageIndic.GetComponent<RectTransform>();
        indicRect.anchoredPosition = Camera.main.WorldToScreenPoint(worldPos);
        TextMeshProUGUI indicText = newDamageIndic.GetComponent<TextMeshProUGUI>();
        indicText.text = dmgVal.ToString() + "!";
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            summonMenu.SetActive(false);
            spellsMenu.SetActive(false);
        }
    }
}
