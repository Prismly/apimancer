    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum CellType
{
    DIRT,
    WATER,
    WALL,
    HONEY,
    LAVA,
    FLOWER
}

public abstract class Cell : Selectable
{
    public abstract CellType Type { get; set; }

    [Header("CELL")]
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
        if (IsOccupied)
        {
            Type occType = Occupant.GetType();
            if (typeof(Unit).IsAssignableFrom(occType))
            {
                Unit occUnit = (Unit)Occupant;
                UIManager.Instance.ShowHealthBox(occUnit);
            }
        }

        //Debug.Log("Hovered");
    }
    public override void OnUnhover()
    {
        //Debug.Log("Unhovered");
        UIManager.Instance.HideHealthBox();
    }

    public override void OnSelect()
    {
        // Debug.Log("Selected");
        // Cell lastCell = (Cell)SelectionManager.Instance.OneSelected;
        // if (lastCell != null && lastCell.IsOccupied && this.Type != CellType.WALL && !IsOccupied && lastCell != this) {
        //     Unit u = (Unit)lastCell.Occupant;
        //     GameManager.Instance.Execute(new MoveAction(ref u, Location));
        // }
        GameManager.Instance.Execute(this);
    }
    // public override void OnDeselect()
    // {
    //     Debug.Log("Deselected");
    // }
}
