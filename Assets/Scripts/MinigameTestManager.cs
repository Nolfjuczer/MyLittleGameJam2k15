using UnityEngine;
using System.Collections;

public class MinigameTestManager : MonoBehaviour
{
    #region Variables
    #endregion

    void Start () 
    {
       
	}
	void Update () 
    {
	
	}

    void OnEnable()
    {
        MinigameManager.OnMinigameWin += Win;
        MinigameManager.OnMinigameLost += Lost;
    }
    void OnDisable()
    {
        MinigameManager.OnMinigameWin -= Win;
        MinigameManager.OnMinigameLost -= Lost;
    }

    void Win()
    {
        Debug.Log("yey won");
    }
    void Lost()
    {
        Debug.Log("you suck");
    }
    public void LaunchMinigame()
    {
        MinigameManager.LaunchMinigame();
    }
}
