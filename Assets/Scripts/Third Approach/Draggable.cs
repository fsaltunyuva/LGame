using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 _mousePoisitionOffset;
    [SerializeField] private GameObject _l1, _l2, _l3, _l4;
    private float _l1OriginalZ, _l2OriginalZ, _l3OriginalZ, _l4OriginalZ;
    private Vector3 startingLoc;
    
    private void Start()
    {
        _l1OriginalZ = _l1.transform.position.z;
        _l2OriginalZ = _l2.transform.position.z;
        _l3OriginalZ = _l3.transform.position.z;
        _l4OriginalZ = _l4.transform.position.z;
        startingLoc = transform.position;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    
    private void OnMouseDown()
    {
        _mousePoisitionOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + _mousePoisitionOffset;
    }

    private void OnMouseUp()
    {
        // Storing the first location of the 4 game objects
        // Vector3 firstLoc = _l1.transform.position;
        // Vector3 firstLoc2 = _l2.transform.position;
        // Vector3 firstLoc3 = _l3.transform.position;
        // Vector3 firstLoc4 = _l4.transform.position;
        
        bool areAllPartsRaycastsCells = !(_l1.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l2.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l3.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l4.GetComponent<Raycast>().currentRaycastedCell is null);

        if (areAllPartsRaycastsCells)
        {
            
            GameObject l1RaycastedCell = _l1.GetComponent<Raycast>().currentRaycastedCell;
            l1RaycastedCell.GetComponent<SpriteRenderer>().color = _l1.GetComponent<SpriteRenderer>().color;
            
            GameObject l2RaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
            l2RaycastedCell.GetComponent<SpriteRenderer>().color = _l2.GetComponent<SpriteRenderer>().color;
            
            GameObject l3RaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
            l3RaycastedCell.GetComponent<SpriteRenderer>().color = _l3.GetComponent<SpriteRenderer>().color;
            
            GameObject l4RaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;
            l4RaycastedCell.GetComponent<SpriteRenderer>().color = _l4.GetComponent<SpriteRenderer>().color;
            
            transform.gameObject.SetActive(false);
            
            // Vector3 positionToBeTransformedTo;
            //
            // GameObject l1RaycastedCell = _l1.GetComponent<Raycast>().currentRaycastedCell;
            // positionToBeTransformedTo = new Vector3(l1RaycastedCell.transform.position.x,
            //     l1RaycastedCell.transform.position.y, _l1OriginalZ);
            // _l1.transform.position = positionToBeTransformedTo;
            //
            // GameObject l2RaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
            // positionToBeTransformedTo = new Vector3(l2RaycastedCell.transform.position.x,
            //     l2RaycastedCell.transform.position.y, _l2OriginalZ);
            // _l2.transform.position = positionToBeTransformedTo;
            //
            // GameObject l3RaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
            // positionToBeTransformedTo = new Vector3(l3RaycastedCell.transform.position.x,
            //     l3RaycastedCell.transform.position.y, _l3OriginalZ);
            // _l3.transform.position = positionToBeTransformedTo;
            //
            // GameObject l4RaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;
            // positionToBeTransformedTo = new Vector3(l4RaycastedCell.transform.position.x,
            //     l4RaycastedCell.transform.position.y, _l4OriginalZ);
            // _l4.transform.position = positionToBeTransformedTo;
            
            // Position Change Debugging
            // Debug.Log(firstLoc - _l1.transform.position);
            // Debug.Log(firstLoc2 - _l2.transform.position);
            // Debug.Log(firstLoc3 - _l3.transform.position);
            // Debug.Log(firstLoc4 - _l4.transform.position);
            
            // Attempt to calculating the center of the 4 game objects and changing the center of the parent object to that
            // but it doesn't work because center calculation is not like what I thought it was
            // Vector3[] playersInGame = {positionToBeTransformedTo1, positionToBeTransformedTo2, positionToBeTransformedTo3, positionToBeTransformedTo4};
            //
            // var totalX = 0f;
            // var totalY = 0f;
            // foreach(var player in playersInGame)
            // {
            //     totalX += player.x;
            //     totalY += player.y;
            // }
            // var centerX = totalX / 4;
            // var centerY = totalY / 4;
            //
            // transform.position = new Vector3(centerX, centerY, transform.position.z);
            
            // Attempt to change the center of the box colliders of the parent but it doesn't work because colliders' center is in local space
            // parentCollider1.center = transform.TransformPoint(_l1.GetComponent<BoxCollider>().center);
            // parentCollider2.center = transform.TransformPoint(_l2.GetComponent<BoxCollider>().center);
            // parentCollider3.center = transform.TransformPoint(_l3.GetComponent<BoxCollider>().center);
            // parentCollider4.center = transform.TransformPoint(_l4.GetComponent<BoxCollider>().center);
        }
        else
        {
            transform.position = startingLoc;
        }
    }
    
}
