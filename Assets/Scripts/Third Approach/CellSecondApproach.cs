using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSecondApproach : MonoBehaviour
{
    public string status = "EMPTY";

    public void UpdateColor()
    {
        switch (status)
        {
            case "EMPTY":
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case "RED":
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "BLUE":
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }

    }
}
