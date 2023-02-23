using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    private enum Team
    { 
        PLAYER,
        COMPUTER,
        NEUTRAL
    }

    // Which team the 
    private Team unitTeam;

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        transform.position += new Vector3(inputVec.x, inputVec.y, 0);
    }
}
