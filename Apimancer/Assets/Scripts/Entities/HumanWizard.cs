using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HumanWizard : Wizard
{
    private int maxHealth = 15;
    private int health = 15;
    private int attackDamage = 1;
    private int movementSpeed = 2;
    private int movementCounter = 2;
    private List<Unit.Faction> targetPriorities = new List<Unit.Faction>();

    private void Awake()
    {
        unitName = "The Apimancer";
        zOffset = 0.57f;
        myShadow.transform.position = new Vector3(myShadow.transform.position.x, myShadow.transform.position.y, zOffset - 0.01f);
    }

    public override void BeginTurn()
    {
        PlaySound(Sounds.Warcry);
        movementCounter = movementSpeed;
        UIManager.Instance.endTurn.GetComponent<Button>().interactable = true;
        IsTurn = true;
        GameManager.Instance.SetCurrentAction(new MoveAction(this));
        foreach(Unit u in Units)
        {
            Bee bee = u as Bee;
            bee.BeginTurn();
        }
    }

    public override void MoveUnits()
    {
        base.MoveUnits();
        foreach (Unit u in Units)
        {
            Bee bee = u as Bee;
            bee.EndTurn();
        }
    }

    public override IEnumerator DetermineMovement()
    {
        // do nothing
        return null;
    }

    public override Action DetermineAction()
    {
        // do nothing
        return null;
    }

    public override int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }

    public override int AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public override int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override List<Unit.Faction> TargetPriorities
    {
        get { return targetPriorities; }
        set { targetPriorities = value; }
    }

    public override void OnSelect()
    {
        if (GameManager.Instance.CurrentAction?.actionType != ActionType.MOVE)
        {
            GameManager.Instance.SetCurrentAction(new MoveAction(this));
        }
        else
        {
            GameManager.Instance.SetCurrentAction(null);
        }
    }
}
