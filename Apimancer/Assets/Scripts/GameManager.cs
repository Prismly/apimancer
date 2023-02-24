using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerControllerPrefab;
    [SerializeField] private List<GameObject> _levelPrefabs;

    private GameObject _playerController;
    private GameObject _currentLevel;

    public int PlayerCount {get; private set;}
    public int CurrentTurn {get; private set;}
    public bool IsRunning {get; private set;}

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
    }

    public void StartGame(int playerCount)
    {
        PlayerCount = playerCount;
        CurrentTurn = 0;
        IsRunning = true;
    }

    public void GameOver()
    {
        IsRunning = false;
    }

    public void LoadLevel(int levelIndex)
    {
        _playerController = Instantiate(_playerControllerPrefab);
        _currentLevel = Instantiate(_levelPrefabs[levelIndex]);
    }

    public int NextTurn()
    {
        CurrentTurn++;
        CurrentTurn %= PlayerCount;

        // Begin players turn
        
        return CurrentTurn;
    }
}
