using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Vector3 rayDirection = new(0,0,10);
    public GameObject currentRaycastedCell;
    
    void FixedUpdate()
    {
        var position = transform.position;

        Debug.DrawRay(position, rayDirection * rayDirection.z, Color.red);
        if (Physics.Raycast(position, rayDirection * rayDirection.z, out var hit))
        {
            // Debug.Log($"Raycast hit:{hit.collider.name} from {gameObject.name}");
            currentRaycastedCell = hit.collider.gameObject;
        }
        else
        {
            currentRaycastedCell = null;
        }
    }
}
