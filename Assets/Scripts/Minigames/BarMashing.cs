using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BarMashing : Minigame
{
    #region Variables

    public Slider mashBar = null;
    public Image[] images = null;
    public Image background = null;

    [Header("Value Tweaking")]
    [Space(20)]
    public float initialProgres = 0.5f;
    public float growRatio = 0.3f;
    
    public float mashValue = 0.33f;
    private bool mashLock = false;

    public float mashValueMultiplier = 0.9f;
    public float growRationMultiplier = 1.1f;

    public float pulseRatio = 1.0f;

    private bool minigamePaused = false;
    private Color backgroundColorDefault = new Color(1.0f,1.0f,1.0f,0.4f);
    private Color backgroundColorLost = new Color(1.0f, 0.1f, 0.1f, 0.4f);
    private Color backgroundColorWin = new Color(0.1f, 1.0f, 0.1f, 0.4f);

    #endregion

    #region Events
    #endregion


    #region Monobehaviour Variables
    void Start () 
    {
        if (this.mashBar == null) this.mashBar = this.GetComponentInChildren<Slider>();
	}

	void Update () 
    {
        if (!this.minigamePaused)
        {
            if (this.mashBar != null)
            {
                float value = this.mashBar.value;
                value += Time.deltaTime * this.growRatio;
                this.mashBar.value = value;
            }
            if (this.images != null)
            {
                for (int i = 0; i < this.images.Length; i++)
                {
                    Color tmpColor = this.images[i].color;
                    tmpColor.a = (1 + Mathf.Sin(Time.time * pulseRatio)) / 2;
                    this.images[i].color = tmpColor;
                }
            }
            if (Input.GetAxis("Fire1") < 0.05f && this.mashLock)
            {
                this.mashLock = false;
            }

            if (Input.GetAxis("Fire1") > 0.05f && !this.mashLock)
            {
                float value = this.mashBar.value;
                value -= this.mashValue;
                this.mashBar.value = value;

                this.mashLock = true;
            }

            if (this.mashBar.value <= 0.0f)
            {
                NotifyOnMinigameWin();
                this.minigamePaused = true;
                this.background.color = this.backgroundColorWin;
                IncreadeDiff();
            }
            if (this.mashBar.value >= 1.0f)
            {
                NotifyOnMinigameLost();
                this.minigamePaused = true;
                this.background.color = this.backgroundColorLost;
                IncreadeDiff();
            }
        }
    }
    
    void OnEnable()
    {
        this.OnMinigameWin += SelfDisable;
        this.OnMinigameLost += SelfDisable;
        ResetMinigame();
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
        this.OnMinigameLost -= SelfDisable;
    }
    #endregion

    #region Methods

    void ResetMinigame()
    {
        if (this.mashBar != null)
        {
            this.mashBar.value = initialProgres;
        }
        this.minigamePaused = false;
        this.background.color = this.backgroundColorDefault;
    }
    void IncreadeDiff()
    {
        this.mashValue *= this.mashValueMultiplier;
        this.growRatio *= this.growRationMultiplier;
    }

    #endregion
}
