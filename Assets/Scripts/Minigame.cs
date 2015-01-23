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
        if (this.OnMinigameLost != null)
        {
            this.OnMinigameLost();
        }
    }

    protected void SelfDisable()
    {
        this.OnMinigameWin = null;
        this.OnMinigameLost = null;
        this.gameObject.SetActive(false);
    }

    #endregion
}
