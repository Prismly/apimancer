using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellManager : MonoBehaviour
{
    private static CellManager instance = null;

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
}
