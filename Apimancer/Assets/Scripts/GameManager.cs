using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        IsRunning = true;
        CurrentTurn = firstTurn;
        CurrentWizard = Wizards[CurrentTurn];
        CurrentWizard.BeginTurn();
    }

    public void GameOver()
    {
        IsRunning = false;
    }

    public void LoadLevel(int levelIndex)
    {
        _playerController = Instantiate(_playerControllerPrefab);
        _currentLevel = Instantiate(_levelPrefabs[levelIndex]).GetComponent<Level>();
    }

    public void EndTurn()
    {
        CurrentWizard.MoveUnits();
    }

    public int NextTurn()
    {
        List<Cell> cells = CellManager.Instance.CellList;
        foreach (Cell c in cells)
        {
            c.OnEndTurn();
        }

        CurrentTurn++;
        CurrentTurn %= WizardCount;
        CurrentWizard = Wizards[CurrentTurn];
        
        CurrentWizard.BeginTurn();
        
        return CurrentTurn;
    }

    public void NotifyNextUnit()
    {
        CurrentWizard.MoveNextUnit();
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
        unit.Wizard = CurrentWizard;

        return unit;
    }

    public Unit SummonUnit(Unit.UnitType type, Vector2Int location)
    {
        return this.SummonUnit(type, CellManager.Instance.GetCell(location));
    }

    public void SetCurrentAction(Action action)
    {
        CurrentAction = action;
    }

    public bool Execute(Cell cell)
    {
        if (CurrentAction == null)
        {
            CurrentAction = new MoveAction(CurrentWizard);
        }
        bool success = CurrentAction.Execute(cell);
        CurrentAction = null;
        return success;
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
}
