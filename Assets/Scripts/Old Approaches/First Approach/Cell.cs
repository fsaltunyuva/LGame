using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum CellState
{
    Empty,
    PreviouslyRed,
    PreviouslyBlue,
    Coin,
    Red,
    Blue
}

public class Cell : MonoBehaviour,
    IPointerDownHandler, 
    IPointerEnterHandler,
    IPointerUpHandler
{
    private MouseHold _mouseHoldInstance;
    [SerializeField] private CellState _cellState = CellState.Empty;

    private void Start()
    {
        int cellNum = int.Parse(Regex.Match(gameObject.name, @"\d+").Value);
        _mouseHoldInstance = GameObject.FindWithTag("Script").GetComponent<MouseHold>();
        
        if(_cellState == CellState.Empty)
            gameObject.GetComponent<Image>().color = _mouseHoldInstance.originalColor;
        
        UpdateColor();
    }

    private void Update()
    {
        if (_mouseHoldInstance.forceColorUpdate)
        {
            UpdateColor();
            _mouseHoldInstance.forceColorUpdate = false;
        }
    }

    public void OnPointerDown (PointerEventData eventData) 
    {
        gameObject.GetComponent<Image>().color = _mouseHoldInstance.mouseDownColor;
        if(!_mouseHoldInstance.currentHoldCells.Contains(gameObject))
            _mouseHoldInstance.AddCellToHold(gameObject);
        
        //_mouseHoldInstance.MarkCell(_mouseHoldInstance.Grid, cellNum, "X");
    }
    
    public void OnPointerEnter (PointerEventData eventData) 
    {
        if (_mouseHoldInstance.mouseHold)
        {
            gameObject.GetComponent<Image>().color = _mouseHoldInstance.mouseDownColor;
            if(!_mouseHoldInstance.currentHoldCells.Contains(gameObject))
                _mouseHoldInstance.AddCellToHold(gameObject);

            //_mouseHoldInstance.MarkCell(_mouseHoldInstance.Grid, cellNum, "X");
        }
    }
    
    public void OnPointerUp (PointerEventData eventData) 
    {
        //Validity Check
        Debug.Log(_mouseHoldInstance.currentHoldCells.Count);
    }

    public void UpdateColor()
    {
        switch (_cellState)
        {
            case CellState.Empty:
                gameObject.GetComponent<Image>().color = _mouseHoldInstance.originalColor;
                break;
            case CellState.PreviouslyRed:
                gameObject.GetComponent<Image>().color = new Color(1,0,0,0.5f);
                break;
            case CellState.PreviouslyBlue:
                gameObject.GetComponent<Image>().color = new Color(0,0,1,0.5f);
                break;
            case CellState.Coin:
                gameObject.GetComponent<Image>().color = new Color(0,1,0);
                break;
            case CellState.Red:
                gameObject.GetComponent<Image>().color = new Color(1,0,0);
                break;
            case CellState.Blue:
                gameObject.GetComponent<Image>().color = new Color(0,0,1);
                break;
        }
    }

}