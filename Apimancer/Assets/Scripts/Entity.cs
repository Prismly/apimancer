using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Cell cell;

    public IEnumerator MoveToPos(Vector3 start, Vector3 end)
    {
        yield return null;
        //List<Cell> open = new List<Cell>();
        //List<Cell> closed = new List<Cell>();
        //Cell current = cell;
        //current.g = 0;
        //current.h = MovementCost(startPoint.position, endPoint.position);

        //// Add the start tile to the open list.
        //openPathTiles.Add(currentTile);

        //while (openPathTiles.Count != 0)
        //{
        //    // Sorting the open list to get the tile with the lowest F.
        //    openPathTiles = openPathTiles.OrderBy(x => x.F).ThenByDescending(x => x.g).ToList();
        //    currentTile = openPathTiles[0];

        //    // Removing the current tile from the open list and adding it to the closed list.
        //    openPathTiles.Remove(currentTile);
        //    closedPathTiles.Add(currentTile);

        //    int g = currentTile.g + 1;

        //    // If there is a target tile in the closed list, we have found a path.
        //    if (closedPathTiles.Contains(endPoint))
        //    {
        //        break;
        //    }

        //    // Investigating each adjacent tile of the current tile.
        //    foreach (Tile adjacentTile in currentTile.adjacentTiles)
        //    {
        //        // Ignore not walkable adjacent tiles.
        //        if (adjacentTile.isObstacle)
        //        {
        //            continue;
        //        }

        //        // Ignore the tile if it's already in the closed list.
        //        if (closedPathTiles.Contains(adjacentTile))
        //        {
        //            continue;
        //        }

        //        // If it's not in the open list - add it and compute G and H.
        //        if (!(openPathTiles.Contains(adjacentTile)))
        //        {
        //            adjacentTile.g = g;
        //            adjacentTile.h = GetEstimatedPathCost(adjacentTile.position, endPoint.position);
        //            openPathTiles.Add(adjacentTile);
        //        }
        //        // Otherwise check if using current G we can get a lower value of F, if so update it's value.
        //        else if (adjacentTile.F > g + adjacentTile.h)
        //        {
        //            adjacentTile.g = g;
        //        }
        //    }
        //}

        //List<Tile> finalPathTiles = new List<Tile>();

        //// Backtracking - setting the final path.
        //if (closedPathTiles.Contains(endPoint))
        //{
        //    currentTile = endPoint;
        //    finalPathTiles.Add(currentTile);

        //    for (int i = endPoint.g - 1; i >= 0; i--)
        //    {
        //        currentTile = closedPathTiles.Find(x => x.g == i && currentTile.adjacentTiles.Contains(x));
        //        finalPathTiles.Add(currentTile);
        //    }

        //    finalPathTiles.Reverse();
        //}

        //return finalPathTiles;
    }

    private int MovementCost(Cell end)
    {
        return 0;
    }

    public void Move()
    {
        Debug.Log("Entity deselected");
        Selectable focused = SelectionManager.Instance.FocusedProspect;
        if (focused)
        {
            StartCoroutine(MoveToPos(transform.position, focused.transform.position));
        }
    }

    // Pathfinding from entity to target cell
    public static Queue<Cell> FindPath(Entity e, Cell target) {
        return null;
    }

    // Move this entity along the given path
    public bool MoveAlongPath(Queue<Cell> path) {
        return false;
    }

    // Move this entity directly to the given cell
    // probably call animation here
    public bool MoveToCell(Cell target) {
        return false;
    }

    // Choose a movement target from the list of entities
    public abstract Cell FindMovementTarget(List<Entity> entities);

    public virtual float GetCellWeight(Cell c) {
        return 1.0;
    }
}
