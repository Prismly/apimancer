using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiningBee : Bee
{
    public static int Cost = 10;
    public static string Name = "Mining Bee";
    public static int maxHealth = 3;
    private int health = 3;
    public static int attackDamage = 3;
    public static int attackRange = 1;
    public static int movementSpeed = 4;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>
            { Unit.Faction.ANT };

    private void Awake()
    {
        unitName = "Mining Bee";
        OnSpawn();
    }

    public override int MaxHealth
    { get { return maxHealth; }
      set { maxHealth = value; } }

    public override int Health
    { get { return health; }
      set { health = value; } }

    public override int AttackDamage
    { get { return attackDamage; }
      set { attackDamage = value; } }

    public override int AttackRange 
    { get { return attackRange; }
      set { attackRange = value; } }

    public override int MovementSpeed
    { get { return movementSpeed; }
      set { movementSpeed = value; } }

    public override List<Unit.Faction> TargetPriorities
    { get { return targetPriorities; }
      set { targetPriorities = value; } }


    // Pathfinding from entity to target cell
    public static new List<Cell> PathFind(Entity e, Cell target)
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
            openPathCells = openPathCells.OrderBy(x => (x.G + x.H)).ThenByDescending(x => x.G).ToList();
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
                // Ignore not walkable, non-boulder adjacent tiles.
                if (adjacentCell.IsOccupied)
                {
                    Unit u = adjacentCell.Occupant as Unit;
                    if (u.Type != UnitType.OTHER_BOULDER)
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
                else if (adjacentCell.G > g)
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

    // Pathfinding from entity to target entity
    public static new List<Cell> PathFind(Entity e, Entity t)
    {
        List<Cell> path = null;
        int dist = int.MaxValue;
        Cell target = t.GetCell();
        List<Cell> adjacents = target.GetAdjacentList();
        foreach (Cell c in adjacents)
        {
            if (!c.IsOccupied || c.Occupant == e || c.Type == CellType.BOULDER)
            {
                List<Cell> tempPath = PathFind(e, c);
                int tempDist = tempPath.Count;
                if (tempDist < dist)
                {
                    dist = tempDist;
                    path = tempPath;
                }
            }
        }
        return path;
    }

    public override IEnumerator DetermineMovement()
    {
        PlaySound(Sounds.Warcry);
        int speed = MovementSpeed;
        if (condition != null)
        {
            switch (condition.condition)
            {
                case Status.Condition.HONEYED:
                    {
                        switch (Type)
                        {
                            case UnitType.ANT_ARMY:
                            case UnitType.ANT_FIRE:
                            case UnitType.ANT_WORKER:
                            case UnitType.ANT_WIZARD:
                            case UnitType.BEE_WIZARD:
                                speed = 1;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                case Status.Condition.WET:
                    speed--;
                    break;
                default:
                    break;
            }
        }
        Tuple<Unit, int, List<Cell>> target = DetermineTarget(speed);
        if (target != null)
        {
            Unit actualTarget = null;
            List<Cell> actualPath = new List<Cell>();
            foreach(Cell c in target.Item3) {
                if (c.IsOccupied) {
                    Unit u = c.Occupant as Unit;
                    if (u == this)
                        continue;
                    actualTarget = u;
                    if (u.Type == UnitType.OTHER_BOULDER) {
                        break;
                    }
                }
                actualPath.Add(c);
            }

            yield return StartCoroutine(MoveAlongPathByAmount(actualPath, speed));
            if (actualPath.Count() < speed + AttackRange)
            {
                if (actualTarget != null && actualTarget.Type == UnitType.OTHER_BOULDER) {
                    AttackTarget(99, actualTarget);
                }
                else {
                    AttackTarget(AttackDamage, target.Item1);
                }
            }
            else RelinquishControl();
        }
        else RelinquishControl();
    }
}
