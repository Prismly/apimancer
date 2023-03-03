using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    public Vector2Int loc { get; set; }

    private static float zOffset = 0.04f;

    public void Start()
    {
        //Tilemap tilemap = transform.parent.GetChild(0).GetComponent<Tilemap>();
        Vector2Int cellPosition = new Vector2Int(loc.x, loc.y);
        transform.position = CellManager.Instance.GetCell(cellPosition).gameObject.transform.position - (Vector3.forward * zOffset);
        // transform.position = tilemap.GetCellCenterWorld(cellPosition) - new Vector3(0, 0, zOffset);
        CellManager.Instance.GetCell(loc).Enter(this);
    }

    protected virtual int MovementCost(Cell c, Cell end)
    {
        Vector3 cPos = c.transform.position;
        Vector3 ePos = end.transform.position;
        return Mathf.RoundToInt(Mathf.Max(Mathf.Abs(cPos.z - ePos.z), Mathf.Max(Mathf.Abs(cPos.x - ePos.x), Mathf.Abs(cPos.y - ePos.y))));
    }

    // Pathfinding from entity to target cell
    public static List<Cell> PathFind(Entity e, Cell target)
    {
        List<Cell> openPathCells = new List<Cell>();
        List<Cell> closedPathCells = new List<Cell>();

        // Prepare the start tile.
        Cell currentCell = CellManager.Instance.GetCell(e.loc);

        currentCell.G = 0;
        currentCell.H = e.MovementCost(currentCell, target);

        // Add the start tile to the open list.
        openPathCells.Add(currentCell);
        while (openPathCells.Count != 0)
        {
            // Sorting the open list to get the tile with the lowest F.
            openPathCells = openPathCells.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            currentCell = openPathCells[0];

            // Removing the current tile from the open list and adding it to the closed list.
            openPathCells.Remove(currentCell);
            closedPathCells.Add(currentCell);

            int g = currentCell.G + 1;

            // If there is a target tile in the closed list, we have found a path.
            if (closedPathCells.Contains(target))
            {
                break;
            }

            // Investigating each adjacent tile of the current tile.
            foreach (Cell adjacentCell in currentCell.GetAdjacentList())
            {
                // Ignore not walkable adjacent tiles.
                if (adjacentCell.IsOccupied)
                {
                    continue;
                }

                // Ignore the tile if it's already in the closed list.
                if (closedPathCells.Contains(adjacentCell))
                {
                    continue;
                }

                // If it's not in the open list - add it and compute G and H.
                if (!(openPathCells.Contains(adjacentCell)))
                {
                    adjacentCell.G = g;
                    adjacentCell.H = e.MovementCost(adjacentCell, target);
                    openPathCells.Add(adjacentCell);
                }
                // Otherwise check if using current G we can get a lower value of F, if so update it's value.
                else if (adjacentCell.F > g + adjacentCell.H)
                {
                    adjacentCell.G = g;
                }
            }
        }

        List<Cell> finalPathCells = new List<Cell>();

        // Backtracking - setting the final path.
        if (closedPathCells.Contains(target))
        {
            currentCell = target;
            finalPathCells.Add(currentCell);

            for (int i = target.G - 1; i >= 0; i--)
            {
                currentCell = closedPathCells.Find(x => x.G == i && currentCell.GetAdjacentList().Contains(x));
                finalPathCells.Add(currentCell);
            }

            finalPathCells.Reverse();
        }

        return finalPathCells;
    }

    public bool MoveToCell(Cell target)
    {
        StartCoroutine(MoveCoroutine(PathFind(this, target)));
        return true;
    }

    private IEnumerator MoveCoroutine(List<Cell> path)
    {
        foreach (Cell c in path)
            yield return StartCoroutine(MoveToOneCell(c));
        Cell last = path.Last();
        loc = last.Location;
        last.Enter(this);
    }

    // Move this entity directly to the given cell
    // probably call animation here
    public IEnumerator MoveToOneCell(Cell target)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 0.25f;
            transform.position = Vector3.Lerp(currentPos, target.transform.position, t);
            yield return null;
        }
    }

    // Choose a movement target from the list of entities
    public abstract Cell FindMovementTarget(List<Entity> entities);

    public virtual float GetCellWeight(Cell c)
    {
        return 1.0f;
    }
}
