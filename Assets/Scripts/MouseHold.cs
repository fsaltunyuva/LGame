using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseHold : MonoBehaviour
{
    [SerializeField] public Color mouseDownColor;
    [SerializeField] public Color originalColor;
    public Stack<GameObject> currentHoldCells = new Stack<GameObject>();
    
    [HideInInspector]
    public bool mouseHold = false;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }
    }
    
    public void AddCellToHold(GameObject cell)
    {
        currentHoldCells.Push(cell);
        Debug.Log($"{cell.name} pushed.");
        
        if(currentHoldCells.Count == 5)
        {
            Debug.Log("5 cells in hold.");
            foreach (GameObject cellInstance in currentHoldCells.ToList())
            {
                cellInstance.GetComponent<Image>().color = originalColor;
                currentHoldCells.Pop();
                Debug.Log($"{cellInstance.name} popped.");
            }
        }
    }
}
