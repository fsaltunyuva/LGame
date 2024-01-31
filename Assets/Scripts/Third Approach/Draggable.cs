using UnityEngine;
using UnityEngine.InputSystem;
using Debug = System.Diagnostics.Debug;

public class Draggable : MonoBehaviour
{
    private Vector3 _mousePoisitionOffset;
    [SerializeField] private GameObject _l1;
    [SerializeField] private GameObject _l2;
    [SerializeField] private GameObject _l3;
    [SerializeField] private GameObject _l4;
    private float _l1originalZ, _l2originalZ, _l3originalZ, _l4originalZ;

    private void Start()
    {
        _l1originalZ = _l1.transform.position.z;
        _l2originalZ = _l2.transform.position.z;
        _l3originalZ = _l3.transform.position.z;
        _l4originalZ = _l4.transform.position.z;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
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
        // Mouse.current.WarpCursorPosition(transform.position);
        //TODO: Fix the box collider position change of the parent object
        bool areAllPartsRaycastsCells = !(_l1.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l2.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l3.GetComponent<Raycast>().currentRaycastedCell is null) &&
                                        !(_l4.GetComponent<Raycast>().currentRaycastedCell is null);

        if (areAllPartsRaycastsCells)
        {
            Vector3 positionToBeTransformedTo;
            
            GameObject l1RaycastedCell = _l1.GetComponent<Raycast>().currentRaycastedCell;
            positionToBeTransformedTo = new Vector3(l1RaycastedCell.transform.position.x,
                l1RaycastedCell.transform.position.y, _l1originalZ);
            _l1.transform.position = positionToBeTransformedTo;
            
            GameObject l2RaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
            positionToBeTransformedTo = new Vector3(l2RaycastedCell.transform.position.x,
                l2RaycastedCell.transform.position.y, _l2originalZ);
            _l2.transform.position = positionToBeTransformedTo;
            
            GameObject l3RaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
            positionToBeTransformedTo = new Vector3(l3RaycastedCell.transform.position.x,
                l3RaycastedCell.transform.position.y, _l3originalZ);
            _l3.transform.position = positionToBeTransformedTo;
            
            GameObject l4RaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;
            positionToBeTransformedTo = new Vector3(l4RaycastedCell.transform.position.x,
                l4RaycastedCell.transform.position.y, _l4originalZ);
            _l4.transform.position = positionToBeTransformedTo;
        }
    }
}
