using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private GameObject rotateButton, mirrorButton, nextTurnButton, skipButton;
    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "Blue's Turn!";
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

    public void ChangeButtons() //Replace the rotate and mirror buttons  with next turn and skip buttons or visa versa
    {
        if (rotateButton.activeSelf)
        {
            rotateButton.SetActive(false);
            mirrorButton.SetActive(false);
            nextTurnButton.SetActive(true);
            skipButton.SetActive(true);
        }
        else
        {
            rotateButton.SetActive(true);
            mirrorButton.SetActive(true);
            nextTurnButton.SetActive(false);
            skipButton.SetActive(false);
        }
    }
}
