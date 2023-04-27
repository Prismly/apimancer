using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerControllerPrefab;
    [SerializeField] private List<GameObject> _levelPrefabs;
    [SerializeField] private List<GameObject> _wizardPrefabs;
    [SerializeField] private List<GameObject> _unitPrefabs;
    [SerializeField] private int _minWizards;
    [SerializeField] private int _maxWizards;

    public bool gameIsPaused = false;

    private GameObject _playerController;
    private Level _currentLevel;
    public Dictionary<Unit.Faction, List<Unit>> Units {get; private set;} = new Dictionary<Unit.Faction, List<Unit>>();

    public Action CurrentAction;
    public int WizardCount {get; private set;}
    public int CurrentTurn {get; private set;}
    public List<Wizard> Wizards {get; private set;}
    public Wizard CurrentWizard {get; private set;}
    public bool IsRunning {get; private set;}
    public bool IsPaused {get; private set;}
    public bool IsUnitMoving {get; private set;}

    private List<Cell> _actionRange = new List<Cell>();
 
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        }
        else 
        { 
            _instance = this;
        }
        LoadLevel(0);
        SpawnWizards(2);
    }

    private void Start()
    {
        // this is here and not in awake because otherwise there will be a nullreference
        StartGame(0);
    }

    private void SpawnWizards(int wizardCount)
    {
        WizardCount = Mathf.Clamp(wizardCount, _minWizards, _maxWizards);
        Wizards = new List<Wizard>();
        for (int i = 0; i < wizardCount; i++)
        {
            Wizard w = Instantiate(_wizardPrefabs[i]).GetComponent<Wizard>();
            List<Unit> factionList = new List<Unit>();
            Units.Add(w.UnitFaction, factionList);
            Units[w.UnitFaction].Add(w);
            Wizards.Add(w);
        }
        _currentLevel.SpawnWizards(Wizards);
    }

    public void StartGame(int firstTurn)
    {
        
        SelectionManager.canInteract = true;
        IsRunning = true;
        CurrentTurn = firstTurn;
        CurrentWizard = Wizards[CurrentTurn];
        CurrentWizard.BeginTurn();
    }

    public void GameOver(bool win)
    {
        SelectionManager.canInteract = false;
        IsRunning = false;
        UIManager.Instance.ShowGameOverMenu(win);
    }

    public void LoadLevel(int levelIndex)
    {
        Units.Add(Unit.Faction.RESOURCE, new List<Unit>());
        Units.Add(Unit.Faction.OTHER, new List<Unit>());
        _playerController = Instantiate(_playerControllerPrefab);
        _currentLevel = Instantiate(_levelPrefabs[levelIndex]).GetComponent<Level>();
    }

    public void EndTurn()
    {
        CurrentWizard.MoveUnits();
        SetCurrentAction(null);
    }

    public int NextTurn()
    {
        List<Cell> cells = CellManager.Instance.CellList;
        foreach (Cell c in cells)
        {
            c.OnEndTurn();
        }

        List<Unit> currentWizardUnits = Units[CurrentWizard.UnitFaction];
        int unitCount = currentWizardUnits.Count;
        for (int i = 0; i < unitCount; i++)
        {
            currentWizardUnits[i].doStatus();
            if (currentWizardUnits.Count < unitCount)
            {
                unitCount = currentWizardUnits.Count;
                i--;
            }
        }

        CurrentTurn++;
        CurrentTurn %= WizardCount;
        CurrentWizard = Wizards[CurrentTurn];
        
        CurrentWizard.BeginTurn();
        
        return CurrentTurn;
    }

    public void NotifyNextUnit()
    {
        if (IsRunning)
        {
            CurrentWizard.MoveNextUnit();
        }
    }

    public int GetUnitCost(Unit.UnitType type)
    {
        switch (type)
        {
        case Unit.UnitType.BEE_WORKER:
            return WorkerBee.Cost;
        case Unit.UnitType.BEE_BUMBLE:
            return BumbleBee.Cost;
        case Unit.UnitType.BEE_MINING:
            return MiningBee.Cost;
        case Unit.UnitType.ANT_WORKER:
            return WorkerAnt.Cost;
        case Unit.UnitType.ANT_ARMY:
            return ArmyAnt.Cost;
        case Unit.UnitType.ANT_FIRE:
            return FireAnt.Cost;
        }
        return -1;
    }

    public int GetUnitHealth(Unit.UnitType type)
    {
        switch (type)
        {
            case Unit.UnitType.BEE_WORKER:
                return WorkerBee.maxHealth;
            case Unit.UnitType.BEE_BUMBLE:
                return BumbleBee.maxHealth;
            case Unit.UnitType.BEE_MINING:
                return MiningBee.maxHealth;
            case Unit.UnitType.ANT_WORKER:
                return WorkerAnt.maxHealth;
            case Unit.UnitType.ANT_ARMY:
                return ArmyAnt.maxHealth;
            case Unit.UnitType.ANT_FIRE:
                return FireAnt.maxHealth;
        }
        return -1;
    }

    public int GetUnitAttack(Unit.UnitType type)
    {
        switch (type)
        {
            case Unit.UnitType.BEE_WORKER:
                return WorkerBee.attackDamage;
            case Unit.UnitType.BEE_BUMBLE:
                return BumbleBee.attackDamage;
            case Unit.UnitType.BEE_MINING:
                return MiningBee.attackDamage;
            case Unit.UnitType.ANT_WORKER:
                return WorkerAnt.attackDamage;
            case Unit.UnitType.ANT_ARMY:
                return ArmyAnt.attackDamage;
            case Unit.UnitType.ANT_FIRE:
                return FireAnt.attackDamage;
        }
        return -1;
    }

    public int GetUnitMove(Unit.UnitType type)
    {
        switch (type)
        {
            case Unit.UnitType.BEE_WORKER:
                return WorkerBee.movementSpeed;
            case Unit.UnitType.BEE_BUMBLE:
                return BumbleBee.movementSpeed;
            case Unit.UnitType.BEE_MINING:
                return MiningBee.movementSpeed;
            case Unit.UnitType.ANT_WORKER:
                return WorkerAnt.movementSpeed;
            case Unit.UnitType.ANT_ARMY:
                return ArmyAnt.movementSpeed;
            case Unit.UnitType.ANT_FIRE:
                return FireAnt.movementSpeed;
        }
        return -1;
    }

    public int GetUnitRange(Unit.UnitType type)
    {
        switch (type)
        {
            case Unit.UnitType.BEE_WORKER:
                return WorkerBee.attackRange;
            case Unit.UnitType.BEE_BUMBLE:
                return BumbleBee.attackRange;
            case Unit.UnitType.BEE_MINING:
                return MiningBee.attackRange;
            case Unit.UnitType.ANT_WORKER:
                return WorkerAnt.attackRange;
            case Unit.UnitType.ANT_ARMY:
                return ArmyAnt.attackRange;
            case Unit.UnitType.ANT_FIRE:
                return FireAnt.attackRange;
        }
        return -1;
    }

    public string GetUnitName(Unit.UnitType type)
    {
        switch (type)
        {
            case Unit.UnitType.BEE_WORKER:
                return WorkerBee.Name;
            case Unit.UnitType.BEE_BUMBLE:
                return BumbleBee.Name;
            case Unit.UnitType.BEE_MINING:
                return MiningBee.Name;
            case Unit.UnitType.ANT_WORKER:
                return WorkerAnt.Name;
            case Unit.UnitType.ANT_ARMY:
                return ArmyAnt.Name;
            case Unit.UnitType.ANT_FIRE:
                return FireAnt.Name;
        }
        return "NaN";
    }

    // TODO: terrible. change later
    public string GetSpellName(SpellAction.SpellType type)
    {
        switch (type)
        {
            case SpellAction.SpellType.HONEY_BLAST:
                return HoneyBlast.sName;
            case SpellAction.SpellType.HONEY_TRAP:
                return HoneyTrap.sName;
            case SpellAction.SpellType.TELEPORT:
                return Teleport.sName;
        }
        return "";
    }

    // TODO: terrible. change later
    public int GetSpellCost(SpellAction.SpellType type)
    {
        switch (type)
        {
            case SpellAction.SpellType.HONEY_BLAST:
                return HoneyBlast.sCost;
            case SpellAction.SpellType.HONEY_TRAP:
                return HoneyTrap.sCost;
            case SpellAction.SpellType.TELEPORT:
                return Teleport.sCost;
        }
        return -1;
    }

    // TODO: terrible. change later
    public uint GetSpellRange(SpellAction.SpellType type)
    {
        switch (type)
        {
            case SpellAction.SpellType.HONEY_BLAST:
                return HoneyBlast.sRange;
            case SpellAction.SpellType.HONEY_TRAP:
                return HoneyTrap.sRange;
            case SpellAction.SpellType.TELEPORT:
                return Teleport.sRange;
        }
        return 0;
    }

    public Unit SummonUnit(Unit.UnitType type, Cell cell)
    {
        Unit unit = Instantiate(_unitPrefabs[(int)type]).GetComponent<Unit>();
        List<Unit> factionList;
        if (Units.ContainsKey(unit.UnitFaction))
        {
            factionList = Units[unit.UnitFaction];
        }
        else
        {
            factionList = new List<Unit>();
            Units.Add(unit.UnitFaction, factionList);
        }
        factionList.Add(unit);
        unit.Commander = CurrentWizard;

        return unit;
    }

    public Unit SummonUnit(Unit.UnitType type, Vector2Int location)
    {
        return this.SummonUnit(type, CellManager.Instance.GetCell(location));
    }

    public void SetCurrentAction(Action action)
    {
        foreach (Cell c in _actionRange)
        {
            c.SetColor(Color.white);
        }

        CurrentAction = action;

        if (action == null)
        {
            _actionRange.Clear();
            return;
        }

        _actionRange = action.unit.GetCell().GetCellsRange(action);
        // Debug.Log("Range: " + action.range);
        Color color;
        switch (action.actionType)
        {
        case ActionType.MOVE:
            color = Color.green;
            break;
        case ActionType.SUMMON:
            color = Color.red;
            break;
        case ActionType.SPELL:
            color = Color.magenta;
            break;
        default:
            return;
        }

        foreach (Cell c in _actionRange)
        {
            c.SetColor(color);
        }
    }

    public bool Execute(Cell cell)
    {
        if (CellInActionRange(cell))
        {
            bool success = CurrentAction.Execute(cell);
            SetCurrentAction(null);
            return success;
        }
        return false;
    }

    public bool CellInActionRange(Cell cell)
    {
        foreach (Cell c in _actionRange)
        {
            if (c.Location == cell.Location)
            {
                return true;
            }
        }
        return false;
    }

    public bool Kill(Unit unit)
    {
        if (unit == null)
        {
            return false;
        }
        if (Units.ContainsKey(unit.UnitFaction))
        {
            List<Unit> factionList = Units[unit.UnitFaction];
            if (factionList != null && factionList.Contains(unit))
            {
                factionList.Remove(unit);
            }
        }
        return true;
    }

    public bool IsPlayersTurn()
    {
        return CurrentWizard == Wizards[0];
    }

    public void OpenScene(string name)
    {
        Debug.Log("Opening Scene " + name);
        SceneManager.LoadScene(name);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void PanicEndTurnButton()
    {
        List<Unit> bees = new List<Unit>();
        Units.TryGetValue(Unit.Faction.BEE, out bees);
        foreach (Unit u in bees)
        {
            u.SetAnimState(Entity.AnimState.IDLE);
        }
        List<Unit> ants = new List<Unit>();
        Units.TryGetValue(Unit.Faction.ANT, out ants);
        foreach (Unit u in ants)
        {
            u.SetAnimState(Entity.AnimState.IDLE);
        }
        foreach (Wizard w in Wizards)
        {
            w.SetAnimState(Entity.AnimState.IDLE);
        }

        NextTurn();
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.B))
        {
            PanicEndTurnButton();
        }
    }
}
