using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerControllerPrefab;
    [SerializeField] private List<GameObject> _levelPrefabs;
    [SerializeField] private List<GameObject> _wizardPrefabs;
    [SerializeField] private int _minWizards;
    [SerializeField] private int _maxWizards;

    private GameObject _playerController;
    private Level _currentLevel;
    public Dictionary<Unit.Faction, List<Unit>> Units {get; private set;} = new Dictionary<Unit.Faction, List<Unit>>();

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
            Wizards.Add(Instantiate(_wizardPrefabs[i]).GetComponent<Wizard>());

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

    public void AddUnit(Unit unit)
    {
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
    }
}
