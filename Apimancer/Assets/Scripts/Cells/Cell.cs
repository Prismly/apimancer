    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum CellType
{
    DIRT,
    WATER,
    BOULDER,
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

    public List<Cell> GetCellsRange(int range)
    {
        List<Cell> rangeCells = new List<Cell>();
        Cell cellHorizontal = this;
        for (int i = 0; i <= range; i++)
        {
            Cell cellVertical = cellHorizontal;
            rangeCells.Add(cellHorizontal);
            for (int j = 0; j < range; j++)
            {
                cellVertical = cellVertical.GetAdjacent(2);
                rangeCells.Add(cellVertical);
            }
            cellVertical = cellHorizontal;
            for (int j = 0; j < range; j++)
            {
                cellVertical = cellVertical.GetAdjacent(4);
                rangeCells.Add(cellVertical);
            }
            cellHorizontal = cellHorizontal.GetAdjacent(0);
        }
        cellHorizontal = this;
        for (int i = 0; i < range; i++)
        {
            cellHorizontal = cellHorizontal.GetAdjacent(3);
            Cell cellVertical = cellHorizontal;
            rangeCells.Add(cellHorizontal);
            for (int j = 0; j < i; j++)
            {
                cellVertical = cellVertical.GetAdjacent(1);
                rangeCells.Add(cellVertical);
            }
            cellVertical = cellHorizontal;
            for (int j = 0; j < i; j++)
            {
                cellVertical = cellVertical.GetAdjacent(5);
                rangeCells.Add(cellVertical);
            }
            cellVertical = cellHorizontal;
        }
        return rangeCells;
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
            Occupant.OnHover();
        }
    }
    public override void OnUnhover()
    {
        if (IsOccupied)
        {
            Occupant.OnUnhover();
        }
    }

    public override void OnSelect()
    {
        if (IsOccupied)
        {
            Occupant.OnSelect();
        }
        GameManager.Instance.Execute(this);
    }
    public override void OnDeselect()
    {
        if (IsOccupied)
        {
            Occupant.OnDeselect();
        }
    }

    public void SetColor(Color color)
    {
        this.GetComponent<SpriteRenderer>().color = color;
    }
    
    public Color GetColor(Color color)
    {
        return this.GetComponent<SpriteRenderer>().color;
    }

    public virtual void OnEndTurn(){}
}
