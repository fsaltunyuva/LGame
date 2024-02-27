using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGameAgainstHuman()
    {
        SceneManager.LoadScene(1);
        Singleton.playAgainstAI = false;
    }
    
    public void StartGameAgainstAI()
    {
        SceneManager.LoadScene(1);
        Singleton.playAgainstAI = true;
    }
}
