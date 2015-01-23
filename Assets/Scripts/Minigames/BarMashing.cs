using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BarMashing : Minigame
{
    #region Variables

    public Slider mashBar = null;
    public Image[] images = null;

    [Header("Value Tweaking")]
    [Space(20)]
    public float initialProgres = 0.5f;
    public float growRatio = 0.3f;
    
    public float mashValue = 0.33f;
    private bool mashLock = false;

    public float pulseRatio = 1.0f;


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
        if(this.mashBar != null)
        {
            float value = this.mashBar.value;
            value += Time.deltaTime * this.growRatio;
            this.mashBar.value = value;
        }
        if(this.images != null)
        {
            for(int i = 0;i < this.images.Length;i++)
            {
                Color tmpColor = this.images[i].color;
                tmpColor.a =  (1 + Mathf.Sin(Time.time * pulseRatio)) / 2;
                this.images[i].color = tmpColor;
            }
        }
        if (Input.GetAxis("Fire1") < 0.05f && this.mashLock)
        {
            this.mashLock = false;
        }
        
        if(Input.GetAxis("Fire1") > 0.05f && !this.mashLock)
        {
            float value = this.mashBar.value;
            value -= this.mashValue;
            this.mashBar.value = value;

            this.mashLock = true;
        }


        if(this.mashBar.value <= 0.0f)
        {
            NotifyOnMinigameWin();
        }
        if (this.mashBar.value >= 1.0f)
        {
            NotifyOnMinigameLost();
        }
    }
    
    void OnEnable()
    {
        if(this.mashBar != null)
        {
            this.mashBar.value = initialProgres;
        }
        this.OnMinigameWin += SelfDisable;
        this.OnMinigameLost += SelfDisable;
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
        this.OnMinigameLost -= SelfDisable;
    }
    #endregion

    #region Methods
    #endregion
}
