using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject l;
    // Vector3 groupCenter;
   

    // private void Start()
    // {
    //     Vector3 sumVector = new Vector3(0f,0f,0f);
    //
    //     foreach (Transform child in l.transform)
    //     {          
    //         sumVector += child.position;        
    //     }
    //
    //     groupCenter = sumVector / l.transform.childCount;
    // }

    public void RotateL()
    {
        // transform.rotation = Quaternion.identity;
        // transform.RotateAround(transform.position + new Vector3(l.transform.GetWidth() / 2f, l.GetHeight() / 2f, 0f), Vector3.forward, angle);
        
        //transform.RotateAround(groupCenter, Vector3.forward, 90);
        l.transform.Rotate(0, 0, 90);
    }
    
    public void MirrorL()
    {
        l.transform.localScale = new Vector3(l.transform.localScale.x, -l.transform.localScale.y, l.transform.localScale.z);
        
    }
}