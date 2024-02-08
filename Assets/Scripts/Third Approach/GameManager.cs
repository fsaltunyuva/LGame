using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private GameObject rotateButton, mirrorButton, nextTurnButton;
    
    [SerializeField] private GameObject coin;
    
    [SerializeField] public string currentColor = "RED";
    
    [SerializeField] public GameObject[] cells;

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "Red's Turn!";
    }

    public void NextTurn()
    {
        //Change the color of L
    }

    // Update is called once per frame
    void Update()
    {
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
            case "coin":
                if(control == 1)
                    coin.GetComponent<Draggable>().canObjectBeMoved = true;
                else
                    coin.GetComponent<Draggable>().canObjectBeMoved = false;
                break;
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
    //* Check Algorithm

}