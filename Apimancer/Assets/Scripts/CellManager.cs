using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Dictionary<Vector2Int, Cell> _cells = new Dictionary<Vector2Int, Cell>();

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

    public void AddCell(Cell cell, Vector2Int location)
    {
        cell.Location = location;
        _cells.Add((Vector2Int)location, cell);
        // Debug.Log("Cell " + _cells.Count + " added");
    }

    public Cell GetCell(Vector2Int location)
    {
        return _cells[location];
    }
}
