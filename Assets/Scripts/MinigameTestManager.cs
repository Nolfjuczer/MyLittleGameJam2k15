using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MinigameTestManager : MonoBehaviour
{
    #region Variables

    public int indexToLaunch = 0;

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
    public void LaunchWithIndex()
    {
        MinigameManager.LaunchMinigame(this.indexToLaunch);
    }
}
