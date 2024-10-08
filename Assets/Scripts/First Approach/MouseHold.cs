using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MouseHold : MonoBehaviour
{
    [SerializeField] public Color mouseDownColor;
    [SerializeField] public Color originalColor;
    public Stack<GameObject> currentHoldCells = new Stack<GameObject>();
    public bool turn = true; //True = Red, False = Blue
    public bool forceColorUpdate = false;

    public string[,] Grid = new string[4, 4]
    {
        { "1", "2", "3", "4" },
        { "5", "6", "7", "8" },
        { "9", "10", "11", "12" },
        { "13", "14", "15", "16" }
    };

    [HideInInspector] public bool mouseHold = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseHold = true;
        }
        else
        {
            mouseHold = false;
        }
    }

    public void AddCellToHold(GameObject cell)
    {
        currentHoldCells.Push(cell);
        
        if (currentHoldCells.Count == 5)
        {
            // foreach (GameObject cellInstance in currentHoldCells.ToList())
            // {
            //     cellInstance.GetComponent<Image>().color = originalColor;
            //     currentHoldCells.Pop();
            // }
        }
    }

    public bool MarkCell(string[,] grid, int cellNumToBeChanged, string valueToChangeTo)
    {
        int counter = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (counter == cellNumToBeChanged)
                {
                    grid[i, j] = valueToChangeTo;
                    //Print2DArray(grid);
                    return true;
                }
                counter++;
            }
        }

        return false;
    }
    
    public void CheckValidityOfMove(int cellNumToBeChanged)
    {
        string[,] candidateGrid = new string[4, 4];
        MarkCell(candidateGrid, cellNumToBeChanged, "X");
        //TODO: Check if the move is valid (move must be horizontal or vertical and not independent)
    }

    public static void Print2DArray(string[,] grid)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sb.Append(grid[i, j]);
                sb.Append(' ');
            }

            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }
    
    public void nextTurn()
    {
        turn = !turn;
        forceColorUpdate = true;
        if (!turn)
        {
            mouseDownColor = Color.blue;
        }
        else
        {
            mouseDownColor = Color.red;
        }
    }
    
}