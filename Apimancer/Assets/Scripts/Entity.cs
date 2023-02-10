using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        transform.position += new Vector3(inputVec.x, inputVec.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
