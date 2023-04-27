using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverAudio : MonoBehaviour
{
    public void PlayAudio(int i)
    {
        AudioSource[] srcs = GetComponents<AudioSource>();
        srcs[i].Play();
    }
}
