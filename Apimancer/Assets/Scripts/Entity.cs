using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] public Cell cell { get; set; }
    protected virtual int MovementCost(Cell c, Cell end)
    {
        Vector3 cPos = c.transform.position;
        Vector3 ePos = end.transform.position;
        return Mathf.RoundToInt(Mathf.Abs(cPos.x - ePos.x) + Mathf.Abs(cPos.y - cPos.y));
    }

    // Pathfinding from entity to target cell
    public static Queue<Cell> FindPath(Entity e, Cell target) {
        List<Cell> open = new List<Cell>();
        List<Cell> closed = new List<Cell>();
        Cell current = e.cell;
        current.G = 0;
        current.H = e.MovementCost(e.cell, target);

        open.Add(current);
        while (open.Count != 0)
        {
            open = open.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            current = open[0];

            open.Remove(current);
            closed.Add(current);

            int g = current.G + 1;

            // If there is a target tile in the closed list, we have found a path.
            if (closed.Contains(target))
                break;

            foreach (Cell adjacent in current.GetAdjacentList())
            {
                // Ignore not walkable adjacent tiles.
                if (adjacent.IsOccupied)
                    continue;

                // Ignore the tile if it's already in the closed list.
                if (closed.Contains(adjacent))
                    continue;

                // If it's not in the open list - add it and compute G and H.
                if (!(open.Contains(adjacent)))
                {
                    adjacent.G = g;
                    adjacent.H = e.MovementCost(adjacent, target);
                    open.Add(adjacent);
                }
                // Otherwise check if using current G we can get a lower value of F, if so update it's value.
                else if (adjacent.F > g + adjacent.H)
                {
                    adjacent.G = g;
                }
            }
        }

        Queue<Cell> finalPathTiles = new Queue<Cell>();

        // Backtracking - setting the final path.
        if (closed.Contains(target))
        {
            current = target;
            finalPathTiles.Enqueue(current);

            for (int i = target.G - 1; i >= 0; i--)
            {
                current = closed.Find(x => x.G == i && current.GetAdjacentList().Contains(x));
                finalPathTiles.Enqueue(current);
            }

            finalPathTiles.Reverse();
        }

        return finalPathTiles;
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
}
