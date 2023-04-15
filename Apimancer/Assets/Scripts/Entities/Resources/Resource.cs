using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : Unit
{
    public enum ResourceType {
        FLOWER
    }

    public static Resource CreateResource(ResourceType resourceType)
    {
        Resource newResource;
        switch (resourceType) {
            case ResourceType.FLOWER:
                newResource = new Flower();
                break;
            default:
                newResource = null;
                break;
        }
        return newResource;
    }

    public override void ReceiveDamage(int dmg) {
        this.Health -= dmg;
        if (this.Health <= 0)
        {
            GameManager.Instance.Kill(this);
            GetCell().Occupant = null;
            Destroy(this.gameObject, 1.0f);
        }
    }
}
