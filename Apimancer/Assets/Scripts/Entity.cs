using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Entity : Cell
{
    public Tilemap grid;

    bool hasMoved;

    void FixedUpdate()
    {
        
    }

    public override void OnSelect()
    {
        Debug.Log("Entity selected");
    }

    public override void OnDeselect()
    {
        Debug.Log("Entity deselected");
        Selectable focused = SelectionManager.Instance.FocusedProspect;
        if (focused)
        {
            transform.position = focused.transform.position;
        }
    }

    public override void OnHover()
    {
        Debug.Log("Hoverin");
    }
}
