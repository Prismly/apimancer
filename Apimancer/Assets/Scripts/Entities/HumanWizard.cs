using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWizard : Wizard
{
    
    public override Faction UnitFaction { get; set; }
    public override float MaxHealth { get; set; }
    public override float Health { get; set; }
    public override float AttackDamage { get; set; }
    public override float MovementSpeed { get; set; }

    public override void BeginTurn()
    {
        IsTurn = true;
    }

    public override Cell FindMovementTarget(List<Entity> entities)
    {
        throw new System.NotImplementedException();
    }
}
