using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField] private Animator ant;
    [SerializeField] private string idleName;

    public void setIdle()
    {
        ant.SetInteger("State", 0);
    }

    public void reset()
    {
        ant.Play(idleName);
        ant.SetInteger("State", 0);
    }

    public void setAttack()
    {
        ant.SetInteger("State", 3);
    }

    public void setWalk()
    {
        ant.SetInteger("State", 1);
    }

    public void setDeath()
    {
        ant.SetInteger("State", 2);
    }
}
