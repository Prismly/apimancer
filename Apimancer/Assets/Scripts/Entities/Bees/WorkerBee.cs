using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBee : Bee
{
    public static float Cost = 5.0f;

    private float maxHealth = 2.0f;
    private float health = 2.0f;
    private float attackDamage = 2.0f;
    private float movementSpeed = 5.0f;

    public override void DetermineAction()
    {
        List<Cell> cells = CellManager.Instance.CellList;
        List<Cell> path = null;
        int dist = short.MaxValue;

        foreach (Cell c in cells) {
            if (c.Type == CellType.FLOWER) {
                List<Cell> tempPath = Entity.PathFind(this, c);
                if (tempPath.Count < dist) {
                    dist = tempPath.Count;
                    path = tempPath;
                }
            }
        }

        if (path != null) {
            float movementRemaining = movementSpeed;
            while (movementRemaining > 0 && path.Count > 0) {
                Cell c = path[0];
                path.RemoveAt(0);
                movementRemaining -= this.GetCellWeight(c);
                if (movementRemaining > 0) {
                    this.MoveToOneCell(c); 
                }
            }
        }
    }

    public override float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override float Health
    {
        get { return health; }
        set { health = value; }
    }

    public override float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public override float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override Cell FindMovementTarget(List<Entity> entities) {
        return null;
    }
}
