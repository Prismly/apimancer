using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity : MonoBehaviour
{
    [SerializeField] public Cell cell { get; set; }

    public static List<Cell> PathFind(Entity entity, Cell end)
    {
        List<Cell> open = new List<Cell>();
        List<Cell> closed = new List<Cell>();
        Cell current = entity.cell;
        current.G = 0;
        current.H = entity.MovementCost(entity.cell, end);

        open.Add(current);
        while (open.Count != 0)
        {
            open = open.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            current = open[0];

            open.Remove(current);
            closed.Add(current);

            int g = current.G + 1;

            // If there is a target tile in the closed list, we have found a path.
            if (closed.Contains(end))
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
                    adjacent.H = entity.MovementCost(adjacent, end);
                    open.Add(adjacent);
                }
                // Otherwise check if using current G we can get a lower value of F, if so update it's value.
                else if (adjacent.F > g + adjacent.H)
                {
                    adjacent.G = g;
                }
            }
        }

        List<Cell> finalPathTiles = new List<Cell>();

        // Backtracking - setting the final path.
        if (closed.Contains(end))
        {
            current = end;
            finalPathTiles.Add(current);

            for (int i = end.G - 1; i >= 0; i--)
            {
                current = closed.Find(x => x.G == i && current.GetAdjacentList().Contains(x));
                finalPathTiles.Add(current);
            }

            finalPathTiles.Reverse();
        }

        return finalPathTiles;
    }

    protected virtual int MovementCost(Cell c, Cell end)
    {
        return 0;
    }

}
