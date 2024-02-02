using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject l;
    
    public void RotateL()
    {
        l.transform.Rotate(0, 0, 90);
    }
    
    public void MirrorL()
    {
        l.transform.localScale = new Vector3(l.transform.localScale.x, -l.transform.localScale.y, l.transform.localScale.z);
        
    }
}