using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            rect.position += Vector3.back * Time.deltaTime * riseDist;
            rect.rotation = Quaternion.LookRotation(rect.localPosition - Camera.main.transform.position, Vector3.back);
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            float alphaRatio = Mathf.Pow(progress / riseTime, 2);
            Debug.Log(alphaRatio);
            Vector4 newColor = text.color;
            newColor.w = 1 - alphaRatio;
            text.color = newColor;
        }
    }
}
