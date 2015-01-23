﻿using UnityEngine;
using System.Collections;
using System;

public class MinigameManager : MonoBehaviour
{
    #region Variables

    public Minigame[] minigames = null;
    [SerializeField]
    private static MinigameManager entity = null;
    int lastMinigameindex = -1;

    #endregion

    #region Events

    public Action _onMinigameWin;
    public Action _onMinigameLost;

    #endregion

    #region Properties

    public static Action OnMinigameWin
    {
        get
        {
            return MinigameManager.entity._onMinigameWin;
        }
        set
        {
            MinigameManager.entity._onMinigameWin = value;
        }
    }
    public static Action OnMinigameLost
    {
        get
        {
            return MinigameManager.entity._onMinigameLost;
        }
        set
        {
            MinigameManager.entity._onMinigameLost = value;
        }
    }
    #endregion

    #region Monobehaviour Methods

    void Awake()
    {
        if (MinigameManager.entity == null) MinigameManager.entity = this;
    }
    void Start () 
    {
        if (MinigameManager.entity == null) MinigameManager.entity = this;
	}
	void Update () 
    {

    }
    #endregion

    #region Methods

    public static void LaunchMinigame(int index = -1)
    {
        if(index == -1)
        {
            MinigameManager.entity.LocalLaunchMinigame();
        }
        else
        {
            MinigameManager.entity.LocallaunchMinigame(index);
        }
    }

    private void LocalLaunchMinigame()
    {
        if (this.minigames != null)
        {
            int randomGameIndex = -1;
            if(this.minigames.Length == 1)
            {
                randomGameIndex = 0;
            }
            else 
            { 
                System.Random ran = new System.Random();
                do
                {
                    randomGameIndex = ran.Next(0, this.minigames.Length - 1);
                } while (this.lastMinigameindex == randomGameIndex);
            }
            LocallaunchMinigame(randomGameIndex);
            this.lastMinigameindex = randomGameIndex;
        }
    }
    private void LocallaunchMinigame(int index)
    {
        if(this.minigames != null)
        {
            if (index >= 0 && index < this.minigames.Length)
            {
                this.minigames[index].gameObject.SetActive(true);
                this.minigames[index].OnMinigameWin += this._onMinigameWin;
                this.minigames[index].OnMinigameLost += this._onMinigameLost;
            }
        }
    }
    
    void NotifyOnMinigameWin()
    {
        if(this._onMinigameWin != null)
        {
            this._onMinigameWin();
        }
    }
    void NotifyOnMinigameLost()
    {
        if(this._onMinigameWin != null)
        {
            this._onMinigameLost();
        }
    }

    #endregion
}
