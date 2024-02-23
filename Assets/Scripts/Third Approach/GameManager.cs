using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private GameObject rotateButton, mirrorButton, nextTurnButton;
    
    [FormerlySerializedAs("coin")] [SerializeField] private GameObject coin1;
    [SerializeField] private GameObject coin2;
    
    [SerializeField] public string currentColor = "RED";
    
    [SerializeField] public GameObject[] cells;
    
    [SerializeField] private GameObject _l1, _l2, _l3, _l4;

    [SerializeField] private GameObject _lFirstParent;
    
    void Start()
    {
        infoText.text = "Red's Turn!";
        
        TestAlgorithm();
    }

    public void ToggleLVisibility(int toggleSwitch)
    {
        Color ogColor = _l1.GetComponent<SpriteRenderer>().color;

        switch (toggleSwitch)
        {
            case 1:
                _l1.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 1);
                _l2.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 1);
                _l3.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 1);
                _l4.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 1);
                break;
            case 0:
                _l1.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 0);
                _l2.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 0);
                _l3.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 0);
                _l4.GetComponent<SpriteRenderer>().color = new Color(ogColor.r, ogColor.g, ogColor.b, 0);
                break;
        }

    }

    public void NextTurn()
    {
        if (currentColor == "RED")
        {
            _l1.GetComponent<SpriteRenderer>().color = Color.blue;
            _l2.GetComponent<SpriteRenderer>().color = Color.blue;
            _l3.GetComponent<SpriteRenderer>().color = Color.blue;
            _l4.GetComponent<SpriteRenderer>().color = Color.blue;
            currentColor = "BLUE";
            infoText.text = "Blue's Turn!";
        }
        else
        {
            _l1.GetComponent<SpriteRenderer>().color = Color.red;
            _l2.GetComponent<SpriteRenderer>().color = Color.red;
            _l3.GetComponent<SpriteRenderer>().color = Color.red;
            _l4.GetComponent<SpriteRenderer>().color = Color.red;
            currentColor = "RED";
            infoText.text = "Red's Turn!";
        }
        
        _lFirstParent.GetComponent<Draggable>().canObjectBeMoved = true;
        ChangeButtons(); //Replace the rotate and mirror buttons with next turn and skip buttons or visa versa
        ToggleLVisibility(1);

        foreach (var cell in GetSpecificColoredCells(currentColor))
        {
            Color cellOgColor = cell.GetComponent<SpriteRenderer>().color;
            cell.GetComponent<SpriteRenderer>().color = new Color(cellOgColor.r, cellOgColor.g, cellOgColor.b, 0.5f);
        }
        
    }

    public void GameOver()
    {
        infoText.text = currentColor + " Wins!";
        Debug.Log("GAME OVER!");
        //TODO: Add game over panel
    }

    public GameObject[] GetSpecificColoredCells(string color){
        List<GameObject> coloredCells = new List<GameObject>();
        foreach (GameObject cell in cells)
        {
            CellSecondApproach cellScript = cell.GetComponent<CellSecondApproach>();
            if (cellScript.status == color)
            {
                coloredCells.Add(cell);
            }
        }

        return coloredCells.ToArray();
    }

    public void ChangeInfoText(string text)
    {
        infoText.text = text;
    }

    public void ChangeButtons() //Replace the rotate and mirror buttons with next turn and skip buttons or visa versa
    {
        if (rotateButton.activeSelf)
        {
            rotateButton.SetActive(false);
            mirrorButton.SetActive(false);
            nextTurnButton.SetActive(true);
            // skipButton.SetActive(true); No need for skip button
        }
        else
        {
            rotateButton.SetActive(true);
            mirrorButton.SetActive(true);
            nextTurnButton.SetActive(false);
            // skipButton.SetActive(false); No need for skip button
        }
    }

    public void MakeGameObjectMovable(string gameObjectName, int control = 1)
    {
        switch (gameObjectName)
        {
            case "coins":
                if (control == 1)
                {
                    coin1.GetComponent<Draggable>().canObjectBeMoved = true;
                    coin2.GetComponent<Draggable>().canObjectBeMoved = true;
                }

                else
                {
                    coin1.GetComponent<Draggable>().canObjectBeMoved = false;
                    coin2.GetComponent<Draggable>().canObjectBeMoved = false;
                }
                break;
            //No need for the "l" case it is done with canObjectMoved = false for now
        }
    }
    
    public string GetOpponentColor()
    {
        if (currentColor == "RED")
            return "BLUE";
        
        return "RED";
    }
    
    public void ClearPreviousCellsStates(GameObject filledL1, GameObject filledL2, GameObject filledL3, GameObject filledL4)
    {
        foreach (GameObject cell in cells)
        {
            CellSecondApproach cellScript = cell.GetComponent<CellSecondApproach>();

            if (cellScript.status == currentColor && cell != filledL1 && cell != filledL2 && cell != filledL3 &&
                cell != filledL4)
            {
                cellScript.status = "EMPTY";
                cellScript.UpdateColor();
            }
            //cellScript.UpdateColor();
        }
    }

    public string[,] GetStatesArray()
    {
        string[,] states = new string[4, 4];
        int i = 0;
        int j = 0;
        foreach (GameObject cell in cells)
        {
            CellSecondApproach cellScript = cell.GetComponent<CellSecondApproach>();
            
            switch (cellScript.status)
            {
                case "EMPTY":
                    states[i, j] = "EMPTY";
                    break;
                case "RED":
                    states[i, j] = "RED";
                    break;
                case "BLUE":
                    states[i, j] = "BLUE";
                    break;
                case "COIN":
                    states[i, j] = "COIN";
                    break;
            }

            j++;
            if (j % 4 == 0)
            {
                i++;
                j = 0;
            }
        }

        return states;
    }

    public void SkipCoinPlacement()
    {
        NextTurn();
    }

    // Check Algorithm
    
    public static int[,,] dirs = new int[4, 3, 2]
    {
        { { 0, 1 }, { 0, 1 }, { 1, 0 } },
        { { 0, 1 }, { 1, 0 }, { 1, 0 } },
        { { 1, 0 }, { 0, 1 }, { 0, 1 } },
        { { 1, 0 }, { 1, 0 }, { 0, 1 } }
    };

    public static int[,] mults = new int[4, 2] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

    public static bool CanPlayerPlace(string[,] array, string color)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (IsValidStart(array, color, i, j))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public static bool IsValidStart(string[,] array, string color, int i, int j)
    {
        string oppColor = color == "RED" ? "BLUE" : "RED";

        for (int m = 0; m < 4; m++)
        {
            for (int d = 0; d < 4; d++)
            {
                bool res = true;
                int x = i, y = j, sameColCounter = 0;
                
                for (int a = 0; a < 3; a++)
                {
                    if (x < 0 || x > 3 || y < 0 || y > 3 || array[x, y] == "COIN" || array[x, y] == oppColor)
                    {
                        res = false;
                        continue;
                    }
                    else if (array[x, y] == color)
                    {
                        sameColCounter++;
                    }

                    x = x + mults[m, 0] * dirs[d, a, 0];
                    y = y + mults[m, 1] * dirs[d, a, 1];
                }

                if (x < 0 || x > 3 || y < 0 || y > 3 || array[x, y] == "COIN" || array[x, y] == oppColor)
                {
                    res = false;
                }
                else if (array[x, y] == color)
                {
                    sameColCounter++;
                }

                if (res && sameColCounter < 4) return true;
            }
        }

        return false;
    }

    private void TestAlgorithm()
    {
        string[,] test1 =
        {
            {"EMPTY", "EMPTY", "COIN", "EMPTY"},
            {"BLUE", "BLUE", "BLUE", "EMPTY"},
            {"RED", "EMPTY", "BLUE", "EMPTY"},
            {"RED", "RED", "RED", "COIN"}
        };
        
        Debug.Log($"Final 1: {CanPlayerPlace(test1, "RED")}");
        
        string[,] test2 =
        {
            {"EMPTY", "EMPTY", "BLUE", "EMPTY"},
            {"COIN", "EMPTY", "BLUE", "EMPTY"},
            {"RED", "BLUE", "BLUE", "COIN"},
            {"RED", "RED", "RED", "EMPTY"}
        };
        Debug.Log($"Final 2: {CanPlayerPlace(test2, "RED")}");
        
        string[,] test3 =
        {
            {"EMPTY", "COIN", "EMPTY", "EMPTY"},
            {"COIN", "BLUE", "EMPTY", "EMPTY"},
            {"RED", "BLUE", "BLUE", "BLUE"},
            {"RED", "RED", "RED", "EMPTY"}
        };
        Debug.Log($"Final 3: {CanPlayerPlace(test3, "RED")}");
        
        string[,] test4 =
        {
            {"EMPTY", "EMPTY", "BLUE", "EMPTY"},
            {"COIN", "EMPTY", "BLUE", "EMPTY"},
            {"RED", "COIN", "BLUE", "BLUE"},
            {"RED", "RED", "RED", "EMPTY"}
        };
        Debug.Log($"Final 4: {CanPlayerPlace(test4, "RED")}");
        //TODO: Test the algorithm on other winning conditions ("https://fr.wikipedia.org/wiki/L_(jeu)")
    }
    
    public void Print2DArray(string[,] arrToPrint)
    {
        StringBuilder sb = new StringBuilder();
        for(int i=0; i< arrToPrint .GetLength(1); i++)
        {
            for(int j=0; j<arrToPrint .GetLength(0); j++)
            {
                sb.Append(arrToPrint [i,j]);
                sb.Append(' ');				   
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
    //* Check Algorithm

}