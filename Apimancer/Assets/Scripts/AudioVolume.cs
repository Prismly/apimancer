using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public static int audioLevel = 50;
    public static UnityEvent levelChanged = null;

    private void Start()
    {
        if (levelChanged == null)
        {
            levelChanged = new UnityEvent();
            levelChanged.AddListener(UpdateVolume);
        }
    }

    public static void ChangeVolume(int amt)
    {
        audioLevel += amt;
        audioLevel = Mathf.Clamp(audioLevel, 0, 100);
        levelChanged.Invoke();
    }

    private void UpdateVolume()
    {
        GetComponent<AudioSource>().volume = audioLevel / 100f;
    }
}
