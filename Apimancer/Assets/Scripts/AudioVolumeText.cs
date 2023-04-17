using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioVolumeText : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Sound: " + AudioVolume.audioLevel.ToString();
    }
}
