using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRoll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float scrollProgress;
    [SerializeField] private float scrollCap;
    [SerializeField] private float skipMulti;
    Vector3 homePos;

    private void Start()
    {
        RectTransform myRect = GetComponent<RectTransform>();
        homePos = myRect.position;
    }

    private void Update()
    {
        float scrollMulti = Input.GetMouseButton(0) ? skipMulti : 1;

        RectTransform myRect = GetComponent<RectTransform>();
        float progThisFrame = scrollSpeed * Time.deltaTime * scrollMulti;
        myRect.anchoredPosition += Vector2.up * progThisFrame;
        scrollProgress += progThisFrame;

        if (scrollProgress >= Screen.height + scrollCap)
        {
            scrollProgress = 0;
            myRect.position = homePos;
        }
    }
}
