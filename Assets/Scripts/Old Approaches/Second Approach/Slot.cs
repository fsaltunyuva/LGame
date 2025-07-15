using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, 
    IDropHandler
    
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag!=null);
        if(eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }
}
