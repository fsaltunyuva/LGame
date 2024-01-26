using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit2D hit;
    
    // Update is called once per frame
    void Update()
    {
        hit = Physics.Raycast(transform.position, new Vector3(0,0, 1));
        
    }
}
