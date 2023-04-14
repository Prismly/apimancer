using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bee : Unit
{
    [SerializeField] private AudioClip sndSummon;

    private Wizard commander;

    private void Awake()
    {
        snd.PlayOneShot(sndSummon);
    }

    public Wizard GetCommander() {
        return commander;
    }
    public void SetCommander(Wizard w)
    {
        commander = w;
    }
}
