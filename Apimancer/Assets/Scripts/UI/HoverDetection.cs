using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("setting to false");
        SelectionManager.canInteract = false;
        SelectionManager.Instance.DeselectAll();
        SelectionManager.Instance.UnhoverAll();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("setting to true");
        SelectionManager.canInteract = true;
    }
}
