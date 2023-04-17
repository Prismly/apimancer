using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GrowTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float bigFont;
    [SerializeField] private float normFont;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("PointerEnter");
        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.fontSize = bigFont;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("PointerExit");
        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.fontSize = normFont;
        }
    }
}
