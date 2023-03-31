using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField] private Animator ant;

    public void setIdle()
    {
        ant.Play("worker_ant_idle");
    }

    public void setAttack()
    {
        ant.Play("worker_ant_attack");
    }

    public void setWalk()
    {
        ant.Play("worker_ant_walk");
    }
}
