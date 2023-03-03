using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWizard : Wizard
{
    
    public override Faction UnitFaction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float AttackDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float MovementSpeed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


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
