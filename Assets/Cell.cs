using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour,
    IPointerDownHandler, 
    //IPointerUpHandler,
    IPointerEnterHandler
{
    private MouseHold _mouseHoldInstance;

    private void Start()
    {
        _mouseHoldInstance = GameObject.FindWithTag("Script").GetComponent<MouseHold>();
        gameObject.GetComponent<Image>().color = _mouseHoldInstance.originalColor;
    }
    
    public void OnPointerDown (PointerEventData eventData) 
    {
        gameObject.GetComponent<Image>().color = _mouseHoldInstance.mouseDownColor;
        _mouseHoldInstance.AddCellToHold(gameObject);
    }

    // public void OnPointerUp (PointerEventData eventData) 
    // {
    //     gameObject.GetComponent<Image>().color = _mouseHoldInstance.originalColor;
    // }
    
    public void OnPointerEnter (PointerEventData eventData) 
    {
        if (_mouseHoldInstance.mouseHold)
        {
            gameObject.GetComponent<Image>().color = _mouseHoldInstance.mouseDownColor;
            _mouseHoldInstance.AddCellToHold(gameObject);
        }
    }
}