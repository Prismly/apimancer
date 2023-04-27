using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class CellManager : MonoBehaviour
{
    private static CellManager instance = null;

    [Serializable]
    public struct CellPrefabsStruct
    {
        public GameObject BoulderTile;
        public GameObject DirtTile;
        public GameObject FlowerTile;
        public GameObject HoneyTile;
        public GameObject LavaTile;
        public GameObject WaterTile;
    }

    [SerializeField]
    private CellPrefabsStruct CellPrefabs;

    public static CellManager Instance {
        get {
            if (instance == null)
            {
                    instance = (CellManager)FindObjectOfType(typeof(CellManager));
            }
            return instance;
        }
    }

    private Dictionary<Vector2Int, Cell> _cellDictionary = new Dictionary<Vector2Int, Cell>();
    public List<Cell> CellList{get; private set;} = new List<Cell>();

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        }
        else 
        { 
            instance = this;
        }
    }

    public bool AddCell(Cell cell, Vector2Int location)
    {
        cell.Location = location;
        try
        {
            _cellDictionary.Add((Vector2Int)location, cell);
            CellList.Add(cell);
            return true;
        } 
        catch (Exception)
        {
            return false;
        }
    }

    public Cell GetCell(Vector2Int location)
    {
        if (_cellDictionary.ContainsKey(location))
        {
            return _cellDictionary[location];
        }
        return null;
    }

    public void ReplaceCell(Vector2Int location, CellType type) {
        Cell oldCell = GetCell(location);
        if (oldCell.IsOccupied)
            return;
        GameObject newTile;
        switch (type)
        {
            case CellType.BOULDER:
                newTile = Instantiate(CellPrefabs.BoulderTile);
                break;
            case CellType.DIRT:
                newTile = Instantiate(CellPrefabs.DirtTile);
                break;
            case CellType.FLOWER:
                newTile = Instantiate(CellPrefabs.FlowerTile);
                break;
            case CellType.HONEY:
                newTile = Instantiate(CellPrefabs.HoneyTile);
                break;
            case CellType.LAVA:
                newTile = Instantiate(CellPrefabs.LavaTile);
                break;
            case CellType.WATER:
                newTile = Instantiate(CellPrefabs.WaterTile);
                break;
            default:
                return;
        }

        Cell newCell = newTile.GetComponent<Cell>();

        newTile.transform.position = oldCell.transform.position;
        newCell.Location = oldCell.Location;
        _cellDictionary.Remove(location);
        _cellDictionary.Add(location, newCell);
        CellList.Remove(oldCell);
        CellList.Add(newCell);
        Destroy(oldCell.gameObject);
    }
}
