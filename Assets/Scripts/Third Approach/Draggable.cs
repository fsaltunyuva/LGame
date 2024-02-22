using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 _mousePoisitionOffset;
    [SerializeField] private GameObject _l1, _l2, _l3, _l4;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] public bool isGameObjectRelatedToL, canObjectBeMoved = true, currentColor;
    private Vector3 locationBeforeDrag;
    private GameObject cellBeforeCoinDrag;


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void OnMouseDown()
    {
        locationBeforeDrag = transform.position; //Only for the coin for now

        if (!isGameObjectRelatedToL) //Namely, coin
            cellBeforeCoinDrag = gameObject.GetComponent<Raycast>().currentRaycastedCell;

        if (canObjectBeMoved)
        {
            _mousePoisitionOffset = transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        if (canObjectBeMoved)
        {
            transform.position = GetMouseWorldPosition() + _mousePoisitionOffset;
        }
    }

    private void OnMouseUp()
    {
        if (!isGameObjectRelatedToL) //Namely, coin
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

                    _gameManager.MakeGameObjectMovable("coin", 0); // Mark coin as unmovable
                    cellBeforeCoinDrag.GetComponent<CellSecondApproach>().status =
                        "EMPTY"; // Update the previous location of the coin as "EMPTY"

                    bool debugForPlacement = GameManager.CanPlayerPlace(_gameManager.GetStatesArray(),
                        _gameManager.GetOpponentColor()); // Update the 2D Array and check if the opponent can place L
                    Debug.Log(debugForPlacement);
                    _gameManager.NextTurn();
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
                            1); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l1CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    // GameObject l2RaycastedCell = _l2.GetComponent<Raycast>().currentRaycastedCell;
                    l2CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            1); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l2CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    //GameObject l3RaycastedCell = _l3.GetComponent<Raycast>().currentRaycastedCell;
                    l3CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            1); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l3CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    //GameObject l4RaycastedCell = _l4.GetComponent<Raycast>().currentRaycastedCell;
                    l4CurrentlyRaycastedCell.GetComponent<SpriteRenderer>().color =
                        new Color(ogColor.r, ogColor.g, ogColor.b,
                            1); //Mark the chosen L locations with faded colors of red or blue (Part 1)
                    l4CurrentlyRaycastedCell.GetComponent<CellSecondApproach>().status = _gameManager.currentColor;

                    canObjectBeMoved = false; //Prevent the player from using L
                    _gameManager.ToggleLVisibility(0); // Change L's location or remove it temporarily
                    _gameManager.ClearPreviousCellsStates(l1CurrentlyRaycastedCell, l2CurrentlyRaycastedCell,
                        l3CurrentlyRaycastedCell, l4CurrentlyRaycastedCell);
                    _gameManager.ChangeInfoText("Move the coin or skip"); //Update the info text
                    _gameManager
                        .ChangeButtons(); //Replace the rotate and mirror buttons with next turn and skip buttons or visa versa
                    _gameManager.MakeGameObjectMovable("coins"); //Mark coins as movable
                }
            }
        }
    }
}