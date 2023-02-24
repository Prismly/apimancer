using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    DIRT,
    WATER,
    WALL
}

public class Cell : Selectable
{
    [Header("CELL")]

    public CellType Type;
    public Vector2Int Location;
    public Entity Occupant;
    public int F;
    public int G;
    public int H;
    public bool IsOccupied => Occupant != null;

    public List<Cell> GetAdjacentList()
    {
        List<Cell> cells = new List<Cell>();

        for (int i = 0; i < 6; i++)
        {
            Cell cell = GetAdjacent(i);
            if (cell != null)
            {
                cells.Add(cell);
            }
        }
        return cells;
    }

    public Cell GetAdjacent(int side)
    {
        if (Location.y % 2 == 0)
        {
            switch (side)
            {
                // CONVENTION: 0 is RIGHT, proceeds counter-clockwise
            case 0:
                return CellManager.Instance.GetCell(Location + Vector2Int.right);
            case 1:
                return CellManager.Instance.GetCell(Location + Vector2Int.up);
            case 2:
                return CellManager.Instance.GetCell(Location + Vector2Int.up + Vector2Int.left);
            case 3:
                return CellManager.Instance.GetCell(Location + Vector2Int.left);
            case 4:
                return CellManager.Instance.GetCell(Location + Vector2Int.down + Vector2Int.left);
            case 5:
                return CellManager.Instance.GetCell(Location + Vector2Int.down);
            default:
                return null;
            }
        }
        else
        {
            switch (side)
            {
            case 0:
                return CellManager.Instance.GetCell(Location + Vector2Int.right);
            case 1:
                return CellManager.Instance.GetCell(Location + Vector2Int.up + Vector2Int.right);
            case 2:
                return CellManager.Instance.GetCell(Location + Vector2Int.up);
            case 3:
                return CellManager.Instance.GetCell(Location + Vector2Int.left);
            case 4:
                return CellManager.Instance.GetCell(Location + Vector2Int.down);
            case 5:
                return CellManager.Instance.GetCell(Location + Vector2Int.down + Vector2Int.right);
            default:
                return null;
            }
        }
    }

    public bool Enter(Entity entity)
    {
        if (IsOccupied) return false;

        this.Occupant = entity;
        OnEnter();

        return true;
    }
    public void Exit()
    {
        this.Occupant = null;
        OnExit();
    }

    public virtual void PassThrough(Entity entity)
    {

    }
    protected virtual void OnEnter()
    {

    }
    protected virtual void OnExit()
    {

    }

    public override void OnHover()
    {
        Debug.Log("Hovered");
    }
    public override void OnUnhover()
    {
        Debug.Log("Unhovered");
    }
    public override void OnSelect()
    {
        Debug.Log("Selected");
    }
    public override void OnDeselect()
    {
        Debug.Log("Deselected");
    }
}
