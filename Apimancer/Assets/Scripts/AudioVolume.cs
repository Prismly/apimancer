using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public static int audioLevel = 50;
    private int cachedAudioLevel = 100;

    public static void ChangeVolume(int amt)
    {
        audioLevel += amt;
        audioLevel = Mathf.Clamp(audioLevel, 0, 100);
    }

    private void Start()
    {
        UpdateVolume();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if (cachedAudioLevel != audioLevel)
        {
            cachedAudioLevel = audioLevel;
            AudioSource src = GetComponent<AudioSource>();
            if (src != null)
            {
                src.volume = audioLevel / 100f;
            }
        }
    }
}
