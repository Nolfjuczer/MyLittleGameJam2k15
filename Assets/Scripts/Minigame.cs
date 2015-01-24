using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Minigame : MonoBehaviour 
{
    #region Variables
    #endregion

    #region Events

    public Action OnMinigameWin;
    public Action OnMinigameLost;

    #endregion


    #region Monobehaviour Variables
    void Start()
    {
    }

    void Update()
    {
    }

    void OnEnable()
    {
        this.OnMinigameWin += SelfDisable;
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
    }
    #endregion

    #region Methods

    public void NotifyOnMinigameWin()
    {
        if (this.OnMinigameWin != null)
        {
            this.OnMinigameWin();
        }
    }
    public void NotifyOnMinigameLost()
    {
        if(!PowerUpManager.CanLostMiniGame())
        {
            if (this.OnMinigameLost != null)
            {
                this.OnMinigameLost();
            }
        }
        else
        {
            NotifyOnMinigameWin();
        }
    }

    protected void SelfDisable()
    {
        Invoke("MakeMeDisabled", 0.3f);
    }
    private void MakeMeDisabled()
    {
        this.OnMinigameWin = null;
        this.OnMinigameLost = null;
        this.gameObject.SetActive(false);
    }

    #endregion
}
