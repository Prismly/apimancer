using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseAndFade : MonoBehaviour
{
    [SerializeField] private float riseDist;
    [SerializeField] private float riseTime;
    private float progress = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        progress += Time.deltaTime;
        if (progress > riseTime)
        {
            Destroy(gameObject);
        }
        else
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.position += Vector3.up * Time.deltaTime * riseDist;
        }
    }
}
