using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RiseAndFade : MonoBehaviour
{
    [SerializeField] private float riseDist;
    [SerializeField] private float riseTime;
    [SerializeField] private float fallThresh;
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
            int riseOrFall = progress < fallThresh ? -1 : 1;

            RectTransform rect = GetComponent<RectTransform>();
            rect.position += Vector3.back * Time.deltaTime * riseDist * riseOrFall;
            rect.rotation = Quaternion.LookRotation(rect.localPosition - Camera.main.transform.position, Vector3.back);

            float alphaRatio = Mathf.Pow(progress / riseTime, 2);
            // Debug.Log(alphaRatio);
            for (int i = 0; i < transform.childCount; i++)
            {
                TextMeshProUGUI text = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                Vector4 newColor = text.color;
                newColor.w = 1 - alphaRatio;
                text.color = newColor;
            }
        }
    }
}
