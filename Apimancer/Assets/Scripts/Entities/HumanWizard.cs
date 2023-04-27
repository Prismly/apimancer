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
    private int movementSpeed = 2;
    public bool hasMoved = false;

    private void Awake()
    {
        
    }

    public override void BeginTurn()
    {
        Vector3 position = this.transform.position;
        position.z = 0;
        CameraController.CameraTransform.position = position;

        PlaySound(Sounds.Warcry);
        hasMoved = false;
        UIManager.Instance.TogglePlayerTurnUI(true);

        IsTurn = true;
        GameManager.Instance.SetCurrentAction(null);
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

    public override int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.Instance.GameOver(false);
    }

    public override void OnSelect()
    {
        GameManager man = GameManager.Instance;
        if (!man.IsPlayersTurn())
        {
            return;
        }

        if (!hasMoved && man.CurrentAction?.actionType != ActionType.MOVE)
        {
            GameManager.Instance.SetCurrentAction(new MoveAction(this, (uint)movementSpeed));
        }
        else
        {
            GameManager.Instance.SetCurrentAction(null);
        }
    }
}
