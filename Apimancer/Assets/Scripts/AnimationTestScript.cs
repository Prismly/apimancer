using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField] private Animator ant;

    public void setIdle()
    {
        ant.Play("Bumble_Bee_Idle");
    }

    public void setAttack()
    {
        ant.Play("Bumble_Bee_Attack");
    }

    public void setWalk()
    {
        ant.Play("Bumble_Bee_Death");
    }
}
