using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Selectable
{
    [Header("CELL")]
    public Vector2Int Location;

    public Entity occupant;

    public Cell Adjacent(int side)
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
        Debug.Log("Selected");}
    public override void OnDeselect()
    {
        Debug.Log("Deselected");
    }
}
