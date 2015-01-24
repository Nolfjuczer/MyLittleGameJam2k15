using UnityEngine;
using System.Collections;
using System;

public class MinigameManager : MonoBehaviour
{
    #region Variables

    public Minigame[] minigames = null;
    [SerializeField]
    private static MinigameManager entity = null;
    int lastMinigameindex = -1;

    private bool _isMinigamePlaying = false;

    //private float defaultTimeMultipler = 1.0f;
    private float _timeMultipler = 1.0f;
    public static float TimeMultiplier
    {
        get
        {
            return MinigameManager.entity._timeMultipler;
        }
    }

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
    public static bool IsMinigamePlaying
    {
        get
        {
            return MinigameManager.entity._isMinigamePlaying;
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
    void OnEnable()
    {
        this._onMinigameWin += MinigameEndHandler;
        this._onMinigameLost += MinigameEndHandler;
    }
    void OnDisable()
    {
        this._onMinigameWin -= MinigameEndHandler;
        this._onMinigameLost -= MinigameEndHandler;
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
                    randomGameIndex = ran.Next(0, this.minigames.Length);
                } while (this.lastMinigameindex == randomGameIndex);
            }
            LocallaunchMinigame(randomGameIndex);
            this.lastMinigameindex = randomGameIndex;
            this._isMinigamePlaying = true;
        }
    }
    private void LocallaunchMinigame(int index)
    {
        if(this.minigames != null)
        {
            if (index >= 0 && index < this.minigames.Length)
            {
                this.minigames[index].gameObject.SetActive(true);
                this.minigames[index].OnMinigameWin += this.WinHandler;
                this.minigames[index].OnMinigameLost += this.LostHandler;
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
    void MinigameEndHandler()
    {
        this._isMinigamePlaying = false;
    }

    void WinHandler()
    {
        NotifyOnMinigameWin();
        this.minigames[this.lastMinigameindex].OnMinigameWin -= WinHandler;
        this.minigames[this.lastMinigameindex].OnMinigameLost -= LostHandler;
    }
    void LostHandler()
    {
        NotifyOnMinigameLost();
        this.minigames[this.lastMinigameindex].OnMinigameWin -= WinHandler;
        this.minigames[this.lastMinigameindex].OnMinigameLost -= LostHandler;
    }

    #endregion
}
