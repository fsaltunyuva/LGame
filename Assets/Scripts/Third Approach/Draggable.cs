using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 _mousePoisitionOffset;
    [SerializeField] private GameObject _l1, _l2, _l3, _l4;
    private float _l1OriginalZ, _l2OriginalZ, _l3OriginalZ, _l4OriginalZ;
    private Vector3 startingLoc;
    private GameObject[] lObjects;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] public bool isGameObjectRelatedToL, canObjectBeMoved = true, currentColor;
    private Vector3 locationBeforeDrag;

    private void Start()
    {
        if (isGameObjectRelatedToL)
        {
            startingLoc = transform.parent.position;
            _l1OriginalZ = _l1.transform.position.z;
            _l2OriginalZ = _l2.transform.position.z;
            _l3OriginalZ = _l3.transform.position.z;
            _l4OriginalZ = _l4.transform.position.z;
            lObjects = new[] { _l1, _l2, _l3, _l4 };
        }
        else
        {
            startingLoc = transform.position;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void OnMouseDown()
    {
        locationBeforeDrag = transform.position; //Only for the coin for now
        
        if (canObjectBeMoved)
        {
            //TODO: Fade the colors of the cells that you started from
            _mousePoisitionOffset = transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        if (canObjectBeMoved)
        {
            transform.position = GetMouseWorldPosition() + _mousePoisitionOffset;
        }


        // foreach (var l in lObjects)
        // {
        //     SpriteRenderer lSpriteRenderer = l.GetComponent<SpriteRenderer>();
        //     Color lSpriteRendererColor = lSpriteRenderer.color;
        //     lSpriteRenderer.color = new Color(lSpriteRendererColor.r, lSpriteRendererColor.g, lSpriteRendererColor.b, 0.5f);
        // }
    }

    private void OnMouseUp()
    {
        // Storing the first location of the 4 game objects
        // Vector3 firstLoc = _l1.transform.position;
        // Vector3 firstLoc2 = _l2.transform.position;
        // Vector3 firstLoc3 = _l3.transform.position;
        // Vector3 firstLoc4 = _l4.transform.position;

        if (!isGameObjectRelatedToL) //Possibly coin
        {
            if (canObjectBeMoved)
            {
                //Coin placement (or Skipping)
                GameObject coinRaycastedCell = gameObject.GetComponent<Raycast>().currentRaycastedCell;

                bool isCoinRaycastsCell = !(coinRaycastedCell is null);
                bool isCellValidForPlacement = false;

                if (isCoinRaycastsCell) //To prevent null reference exception
                    isCellValidForPlacement =
                        coinRaycastedCell.GetComponent<CellSecondApproach>().status.Equals("EMPTY");

                if (isCoinRaycastsCell &&
                    isCellValidForPlacement) //Prevent the player placing the coin on invalid cells
                {
                    Vector3 positionToBeTransformedTo = new Vector3(coinRaycastedCell.transform.position.x,
                        coinRaycastedCell.transform.position.y, transform.position.z);
                    transform.position = positionToBeTransformedTo;
                    coinRaycastedCell.GetComponent<CellSecondApproach>().status = "COIN";
                    
                    //TODO: Mark coin as unmovable
                    //TODO: Update the 2D Array
                    //TODO: Check if the opponent can place L
                    _gameManager.NextTurn();
                    //TODO: Fade back the colors of the cells that you have changed in part 1 to their og colors
                    //TODO: Change the color of the L
                }
                else
                {
                    transform.position = locationBeforeDrag;
                }
                //Ensured that the player moved only one coin by going to next turn after placing the coin
            }
        }
        else
        {
            if (canObjectBeMoved)
            {
                GameObject l1CurrentlyRaycastedCell = _l1.GetComponent<Raycast>().currentRaycastedCell;
                GameObject l2CurrentlyRaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
                GameObject l3CurrentlyRaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
                GameObject l4CurrentlyRaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;

                bool areAllPartsRaycastsCells = !(l1CurrentlyRaycastedCell is null) &&
                                                !(l2CurrentlyRaycastedCell is null) &&
                                                !(l3CurrentlyRaycastedCell is null) &&
                                                !(l4CurrentlyRaycastedCell is null);

                bool areCellsValidForPlacement = false;
                bool areAllPartsRaycastsCellsCurrentColor = true;
                if (areAllPartsRaycastsCells) //To prevent null reference exception
                {
                    string l1CurrentlyRaycastedCellStatus =
                        l1CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status;
                    string l2CurrentlyRaycastedCellStatus =
                        l2CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status;
                    string l3CurrentlyRaycastedCellStatus =
                        l3CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status;
                    string l4CurrentlyRaycastedCellStatus =
                        l4CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status;

                    //Create a bool to check if the ray casted cells are valid to place L
                    areCellsValidForPlacement =
                        !(l1CurrentlyRaycastedCellStatus.Equals(_gameManager.GetOpponentColor()) ||
                          l1CurrentlyRaycastedCellStatus.Equals("COIN")) &&
                        !(l2CurrentlyRaycastedCellStatus.Equals(_gameManager.GetOpponentColor()) ||
                          l2CurrentlyRaycastedCellStatus.Equals("COIN")) &&
                        !(l3CurrentlyRaycastedCellStatus.Equals(_gameManager.GetOpponentColor()) ||
                          l3CurrentlyRaycastedCellStatus.Equals("COIN")) &&
                        !(l4CurrentlyRaycastedCellStatus.Equals(_gameManager.GetOpponentColor()) ||
                          l4CurrentlyRaycastedCellStatus.Equals("COIN"));

                    //Create a bool to check if all ray casted cells are casting the current colors
                    areAllPartsRaycastsCellsCurrentColor =
                        l1CurrentlyRaycastedCellStatus.Equals(_gameManager.currentColor) &&
                        l2CurrentlyRaycastedCellStatus.Equals(_gameManager.currentColor) &&
                        l3CurrentlyRaycastedCellStatus.Equals(_gameManager.currentColor) &&
                        l4CurrentlyRaycastedCellStatus.Equals(_gameManager.currentColor);
                }

                Debug.Log("Are all parts ray casts cells: " + areAllPartsRaycastsCells);
                Debug.Log("Are cells valid for placement: (Due to Game Logic)" + areCellsValidForPlacement);
                Debug.Log("Are all parts ray casts cells current color: " + areAllPartsRaycastsCellsCurrentColor);

                if (areAllPartsRaycastsCells && areCellsValidForPlacement &&
                    !areAllPartsRaycastsCellsCurrentColor) //Prevent the player placing the L on invalid cells
                {
                    //NOT: L'nin sadece childları Z'de dışarıda parent 0da
                    Color ogColor = _l1.GetComponent<SpriteRenderer>().color;

                    // GameObject l1RaycastedCell = _l1.GetComponent<Raycast>().currentRaycastedCell;
                    l1CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            0.5f); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l1CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    // GameObject l2RaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
                    l2CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            0.5f); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l2CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    //GameObject l3RaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
                    l3CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            0.5f); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l3CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    //GameObject l4RaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;
                    l4CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            0.5f); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l4CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;


                    canObjectBeMoved = false; //Prevent the player from using L
                    //TODO: Change L's location or remove it temporarily
                    _gameManager.ClearPreviousCellsStates(l1CurrentlyRaycastedCell, l2CurrentlyRaycastedCell,
                        l3CurrentlyRaycastedCell, l4CurrentlyRaycastedCell);
                    _gameManager.ChangeInfoText("Move the coin or skip"); //Update the info text
                    _gameManager
                        .ChangeButtons(); //Replace the rotate and mirror buttons with next turn and skip buttons or visa versa
                    _gameManager.MakeGameObjectMovable("coin"); //Mark coin as movable
                }


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
                // transform.parent.position = startingLoc;
            }
        }
    }
}