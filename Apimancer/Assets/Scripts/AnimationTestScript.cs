using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField] private Animator ant;

    public void setIdle()
    {
        ant.Play("Fire Ant Idle");
        ant.SetBool("moving", false);
    }

    public void setAttack()
    {
        ant.SetTrigger("attack");
    }

    public void setWalk()
    {
        ant.SetBool("moving", true);
    }

    public void setDeath()
    {
        ant.SetTrigger("death");
    }
}
