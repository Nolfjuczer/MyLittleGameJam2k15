using UnityEngine;
using System.Collections;

public class AnalogSpin : Minigame
{

    #region Monobehaviour Methods
    void Start () 
    {
	
	}
	void Update () 
    {
	
	}
    void OnEnable()
    {
        this.OnMinigameWin += SelfDisable;
        this.OnMinigameLost += SelfDisable;
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
        this.OnMinigameLost -= SelfDisable;
    }
    #endregion
}
