using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWizard : Wizard
{
    public override void BeginTurn()
    {
        IsTurn = true;
    }

    public override void EndTurn()
    {
        IsTurn = false;
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        throw new System.NotImplementedException();
    }
}
