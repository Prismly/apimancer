using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas;
    [SerializeField] private GameObject targetWorldCanvas;

    [SerializeField] private GameObject menuBoxPref;
    [SerializeField] private GameObject healthBox;
    [SerializeField] public GameObject endTurn;

    [SerializeField] private List<GameObject> disabledOnPause;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    [SerializeField] private RectTransform bottomMid;
    [SerializeField] private GameObject damageIndic;

    [SerializeField] public GameObject HEAD_HOR;
    [SerializeField] public GameObject HEAD_DIAG;
    [SerializeField] public GameObject BODY_DIAG;
    [SerializeField] public GameObject BODY_HOR;
    [SerializeField] public GameObject BODY_CURVE;

    [SerializeField] public GameObject summonMenuButton = null;
    [SerializeField] public GameObject spellsMenuButton = null;
    private GameObject summonMenu = null;
    private GameObject spellsMenu = null;
    private bool summonMenuVis = false;
    private bool spellsMenuVis = false;
    //private GameObject healthBox;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public SoundStruct Sounds;

    [Serializable] 
    public struct SoundStruct 
    {
        public AudioClip ValidClick;
        public AudioClip InvalidClick;
        public AudioClip SummonMenu;
        public AudioClip SpellsMenu;
    }

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
        //spellsMenuBox.AddSpellsOption("[X]Honey Trap [NCT]3 [RNG]3", playerWiz, SpellAction.SpellType.HONEY_TRAP, 3, 3);
        //spellsMenuBox.AddSpellsOption("[X]Honey Blast [NCT]3 [RNG]3", playerWiz, SpellAction.SpellType.HONEY_BLAST, 3, 3);
        //spellsMenuBox.AddSpellsOption("[X]Teleport * 3 [RNG]3", playerWiz, SpellAction.SpellType.TELEPORT, 3, 3);
        SpellAction.SpellType[] spellTypes = { SpellAction.SpellType.HONEY_TRAP, SpellAction.SpellType.HONEY_BLAST, SpellAction.SpellType.TELEPORT };
        for (int i = 0; i < spellTypes.Length; i++)
        {
            StringBuilder spellStr = new StringBuilder();
            string uName = GameManager.Instance.GetSpellName(spellTypes[i]);
            int uCost = GameManager.Instance.GetSpellCost(spellTypes[i]);
            uint uRng = (uint)GameManager.Instance.GetSpellRange(spellTypes[i]);
            spellStr.Append("[X]");
            spellStr.Append(uName);
            spellStr.Append(" [STR]");
            spellStr.Append(uCost);
            spellStr.Append(" [RNG]");
            spellStr.Append(uRng);
            spellsMenuBox.AddSpellsOption(spellStr.ToString(), playerWiz, spellTypes[i], uRng, uCost);
        }
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
        Unit.UnitType[] unitSummons = { Unit.UnitType.BEE_WORKER, Unit.UnitType.BEE_BUMBLE, Unit.UnitType.BEE_MINING };
        for (int i = 0; i < unitSummons.Length; i++)
        {
            StringBuilder summonStr = new StringBuilder();
            string uName = GameManager.Instance.GetUnitName(unitSummons[i]);
            int uCost = GameManager.Instance.GetUnitCost(unitSummons[i]);
            int uHP = GameManager.Instance.GetUnitHealth(unitSummons[i]);
            int uAtk = GameManager.Instance.GetUnitAttack(unitSummons[i]);
            uint uRng = (uint) GameManager.Instance.GetUnitRange(unitSummons[i]);
            int uMov = GameManager.Instance.GetUnitMove(unitSummons[i]);
            summonStr.Append("[X]");
            summonStr.Append(uName);
            summonStr.Append(" [STR]");
            summonStr.Append(uCost);
            summonStr.Append(" [HPA]");
            summonStr.Append(uHP);
            summonStr.Append(" [ATK]");
            summonStr.Append(uAtk);
            summonStr.Append(" [RNG]");
            summonStr.Append(uRng);
            summonStr.Append(" [MOV]");
            summonStr.Append(uMov);
            summonMenuBox.AddSummonOption(summonStr.ToString(), playerWiz, unitSummons[i], uRng, uCost);
        }
        //summonMenuBox.AddSummonOption("[X]Worker Bee [STR]5 [HPA]2 [ATK]2 [MOV]5", playerWiz, Unit.UnitType.BEE_WORKER, 1, 5);
        //summonMenuBox.AddSummonOption("[X]Bumble Bee [STR]8 [HPA]5 [ATK]2 [MOV]3", playerWiz, Unit.UnitType.BEE_BUMBLE, 1, 8);
        //summonMenuBox.AddSummonOption("[X]Miner Bee [STR]10 [HPA]3 [ATK]4 [MOV]5", playerWiz, Unit.UnitType.BEE_MINING, 1, 10);
        summonMenu.SetActive(false);
    }
    
    public void TogglePlayerMove()
    {
        // the move button has been clicked
    }

    public void ToggleSpellsMenu()
    {
        if (!GameManager.Instance.IsPlayersTurn())
        {
            return;
        }

        summonMenu.SetActive(false);
        if (spellsMenu.activeInHierarchy) {
            spellsMenu.SetActive(false);
            PlaySound(Sounds.ValidClick);
        }
        else {
            spellsMenu.SetActive(true);
            PlaySound(Sounds.SpellsMenu);
        }
    }

    public void ToggleSummonMenu()
    {
        if (!GameManager.Instance.IsPlayersTurn())
        {
            return;
        }

        spellsMenu.SetActive(false);
        if (summonMenu.activeInHierarchy) {
            summonMenu.SetActive(false);
            PlaySound(Sounds.ValidClick);
        }
        else {
            summonMenu.SetActive(true);
            PlaySound(Sounds.SummonMenu);
        }
    }

    public void EndPlayerTurn()
    {
        PlaySound(Sounds.ValidClick);
        TogglePlayerTurnUI(false);
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
        if (target.condition != null)
        {
            switch (target.condition.condition)
            {
                case Status.Condition.HONEYED:
                    {
                        healthBoxText.text = "[NCT]" + "<sprite=6>" + target.Health + "/" + target.MaxHealth;
                        break;
                    }
                case Status.Condition.BURNED:
                    {
                        healthBoxText.text = "[BRN]" + "<sprite=6>" + target.Health + "/" + target.MaxHealth;
                        break;
                    }
                case Status.Condition.WET:
                    {
                        healthBoxText.text = "[WTR]" + "<sprite=6>" + target.Health + "/" + target.MaxHealth;
                        break;
                    }
            }
        }
        else
        {
            healthBoxText.text = "<sprite=6>" + target.Health + "/" + target.MaxHealth;
        }
        healthBoxText.text = OptionAddon.Mutate(healthBoxText.text);
        healthBox.SetActive(true);
    }

    public void HideHealthBox()
    {
        if (healthBox != null)
        {
            healthBox.SetActive(false);
        }

    }

    public void ShowGameOverMenu(bool win)
    {
        if (win)
            winMenu.SetActive(true);
        else
            loseMenu.SetActive(true);
    }

    public void TogglePause()
    {
        PlaySound(Sounds.ValidClick);
        if (GameManager.Instance.gameIsPaused)
        {
            GameManager.Instance.gameIsPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            foreach (GameObject g in disabledOnPause)
            {
                g.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            GameManager.Instance.gameIsPaused = true;
            summonMenu.SetActive(false);
            spellsMenu.SetActive(false);
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
        //Debug.Log("Spawning DamageIndicator");
        GameObject newDamageIndic = Instantiate(damageIndic);
        newDamageIndic.transform.SetParent(targetWorldCanvas.transform);
        RectTransform indicRect = newDamageIndic.GetComponent<RectTransform>();
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

        if (dmgVal > 0)
        {
            // Red; damage
            Debug.Log("Red");
            newDamageIndic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(67f / 255f, 12f / 255f, 12f / 255f, 255);
            newDamageIndic.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(236f / 255f, 88f / 255f, 61f / 255f, 255);
        }
        else if (dmgVal < 0)
        {
            // Green; healing
            Debug.Log("Green");
            newDamageIndic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(12f / 255f, 67f / 255f, 13f / 255f, 255);
            newDamageIndic.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(132f / 255f, 236f / 255f, 61f / 255f, 255);
        }
        else
        {
            // Grey; no damage
            Debug.Log("Grey");
            newDamageIndic.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(58f / 255f, 58f / 255f, 58f / 255f, 255f);
            newDamageIndic.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(137f / 255f, 137f / 255f, 137f / 255f, 255f);
        }

        indicRect.localPosition = worldPos;
        //indicRect.localPosition = new Vector3(screenPoint.x - (targetCanvas.GetComponent<Canvas>().pixelRect.width / 2), screenPoint.y - (targetCanvas.GetComponent<Canvas>().pixelRect.height / 2), 0);
        for (int i = 0; i < newDamageIndic.transform.childCount; i++)
        {
            TextMeshProUGUI indicText = newDamageIndic.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            indicText.text = (dmgVal > 0 ? "-" : "+") + Mathf.Abs(dmgVal).ToString() + "!";
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            summonMenu.SetActive(false);
            spellsMenu.SetActive(false);
        }
    }

    public void PlaySound(AudioClip sound) {
        audioSource.PlayOneShot(sound);
    }

    public void BackToMainMenu()
    {
        TogglePause();
        GameManager.Instance.OpenScene("MainMenu");
    }

    public void IncrementSound(bool goingUp)
    {
        Debug.Log(goingUp);
        AudioVolume.ChangeVolume(goingUp ? 25 : -25);
    }

    public void TogglePlayerTurnUI(bool val)
    {
        if (!val)
        {
            summonMenu.SetActive(false);
            spellsMenu.SetActive(false);
        }

        if (endTurn != null)
        {
            endTurn.GetComponent<Button>().interactable = val;
        }
        if (summonMenuButton != null)
        {
            summonMenuButton.GetComponent<Button>().interactable = val;
        }
        if (spellsMenuButton != null)
        {
            spellsMenuButton.GetComponent<Button>().interactable = val;
        }
    }
}
