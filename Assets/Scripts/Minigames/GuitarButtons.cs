using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GuitarButtons : Minigame 
{
    const int button_A = 0;
    const int button_B = 1;
    const int button_X = 2;
    const int button_Y = 3;

    public GameObject GObuttonA = null;
    public GameObject GObuttonB = null;
    public GameObject GObuttonX = null;
    public GameObject GObuttonY = null;

    public GameObject buttonsLine = null;
    private bool minigamePaused = false;

    public Vector3 StartPos = Vector3.zero;
    public Vector3 EndPos = Vector3.zero;

    public Image backgroundImage = null;
    public Image frameImage = null;
    public Text hitCountText = null;

    private List<Button> buttonsProcessed = new List<Button>();

    [Header("Params")]
    [Space(10)]
    private int _buttonHitCount = 0;
    public int buttonHitCount
    {
        get
        {
            return this._buttonHitCount;
        }
        set
        {
            this._buttonHitCount = value;
            NotifyOnButtonHitCountValueChanged(this._buttonHitCount);
        }
    }
    public Action<int> OnButtonHitCountValueChanged;

    private float timeElapsed = 0.0f;
    private float timeIntervalElapsed = 0.0f;
    public float timeInterval = 1.0f;
    public float buttonsSpeed = 1.0f;

    [Header("Win Lost Params")]
    [Space(10)]
    public int hitCountTarget = 5;
    public int hitCountLost = -5;
    public int hitCountLevelAmount = 1;

    private bool lock_fire1 = false;
    private bool lock_fire2 = false;
    private bool lock_fire3 = false;
    private bool lock_fire4 = false;

    [Header("ButtonHitFrame")]
    [Space(10)]
    public float frameMin = 0.4f;
    public float frameMax = 0.6f;

    private bool areEffectsOn = false;
    private float effectLength = 0.3f;
    private float effectTimeElapsed =0.0f;
    private Color delaultColorBackground = new Color(1.0f, 1.0f, 0.0f, 0.6f);
    private Color delaultColorFrame = new Color(0.4f, 0.4f, 0.4f, 1.0f);

    public Action OnGuitarButtonHit;
    public Action OnGuitarButtonMiss;

    void NotifyOnGuitarButtonHit()
    {
        if (this.OnGuitarButtonHit != null)
        {
            this.OnGuitarButtonHit();
        }
    }
    void NotifyOnGuitarButtonMiss()
    {
        if (this.OnGuitarButtonMiss != null)
        {
            this.OnGuitarButtonMiss();
        }
    }
    void NotifyOnButtonHitCountValueChanged(int value)
    {
        if(this.OnButtonHitCountValueChanged != null)
        {
            this.OnButtonHitCountValueChanged(value);
        }
    }

    void Start () 
    {
        
	}
	void Update () 
    {
        if (!this.minigamePaused)
        {


            this.timeElapsed += Time.deltaTime;
            this.timeIntervalElapsed += Time.deltaTime;
            this.effectTimeElapsed += Time.deltaTime;

            if (this.effectTimeElapsed > this.effectLength && this.areEffectsOn)
            {
                this.backgroundImage.color = this.delaultColorBackground;
                this.frameImage.color = this.delaultColorFrame;
                this.areEffectsOn = false;
            }

            if (this.buttonsProcessed != null)
            {
                for (int i = 0; i < this.buttonsProcessed.Count; i++)
                {
                    this.buttonsProcessed[i].progres += Time.deltaTime * buttonsSpeed;

                    Vector3 pos = this.buttonsProcessed[i].rectTransform.localPosition;
                    pos = Vector3.Lerp(this.StartPos, EndPos, this.buttonsProcessed[i].progres);
                    this.buttonsProcessed[i].rectTransform.localPosition = pos;

                    Color tmpColor = this.buttonsProcessed[i].image.color;
                    tmpColor.a = Mathf.Sin(this.buttonsProcessed[i].progres * Mathf.PI);
                    this.buttonsProcessed[i].image.color = tmpColor;
                }
            }

            if (this.timeIntervalElapsed >= this.timeInterval)
            {
                this.timeIntervalElapsed -= this.timeInterval;
                AddButton();
            }
            for (int i = 0; i < this.buttonsProcessed.Count; i++)
            {
                if (this.buttonsProcessed[i].progres >= 1.0f)
                {
                    
                    this.buttonHitCount--;
                    this.buttonsProcessed[i].image.color = Color.red;
                    this.buttonsProcessed[i].SeldDestroy((1/this.buttonsSpeed) / 2);
                    this.buttonsProcessed.RemoveAt(i);
                    i--;
                    if (this.buttonHitCount <= this.hitCountLost)
                    {
                        NotifyOnMinigameLost();
                        this.minigamePaused = true;
                    }
                }
            }

            if (Input.GetAxis("Fire1") < 0.05f && this.lock_fire1)
            {
                this.lock_fire1 = false;
            }
            if (Input.GetAxis("Fire1") > 0.05f && !this.lock_fire1)
            {
                this.lock_fire1 = true;
                CheckButtonPressed(button_A);
            }


            if (Input.GetAxis("Fire2") < 0.05f && this.lock_fire2)
            {
                this.lock_fire2 = false;
            }
            if (Input.GetAxis("Fire2") > 0.05f && !this.lock_fire2)
            {
                this.lock_fire2 = true;
                CheckButtonPressed(button_B);
            }

            if (Input.GetAxis("Fire3") < 0.05f && this.lock_fire3)
            {
                this.lock_fire3 = false;
            }
            if (Input.GetAxis("Fire3") > 0.05f && !this.lock_fire3)
            {
                this.lock_fire3 = true;
                CheckButtonPressed(button_X);
            }

            if (Input.GetAxis("Fire4") < 0.05f && this.lock_fire4)
            {
                this.lock_fire4 = false;
            }
            if (Input.GetAxis("Fire4") > 0.05f && !this.lock_fire4)
            {
                this.lock_fire4 = true;
                CheckButtonPressed(button_Y);
            }



            if (this.buttonHitCount >= this.hitCountTarget)
            {
                NotifyOnMinigameWin();
            }
        }
	}

    void CheckButtonPressed(int type)
    {
        if(this.buttonsProcessed != null)
        {
            bool wasAnythingHit = false;
            for(int i = 0;i < this.buttonsProcessed.Count;i++)
            {
                if(this.buttonsProcessed[i].progres >= this.frameMin && this.buttonsProcessed[i].progres <= frameMax && this.buttonsProcessed[i].type == type)
                {
                    wasAnythingHit = true;
                    NotifyOnGuitarButtonHit();
                    GameObject.Destroy(this.buttonsProcessed[i].gameObject);
                    this.buttonsProcessed.RemoveAt(i);
                    return;
                }
            }
            if(!wasAnythingHit)
            {
                NotifyOnGuitarButtonMiss();
            }
        }
    }


    void AddButton(int type = -1)
    {
        Button tmpButton = null;
        GameObject tmpGO = null;

        System.Random rand = new System.Random();
        if(type == -1)
        {
            type = rand.Next(0, 4);
        }

        switch(type)
        {
            case button_A:
                tmpGO = (GameObject) GameObject.Instantiate(this.GObuttonA);
                tmpButton = tmpGO.GetComponent<Button>();
                break;
            case button_B:
                tmpGO = (GameObject) GameObject.Instantiate(this.GObuttonB);
                tmpButton = tmpGO.GetComponent<Button>();
                break;
            case button_X:
                tmpGO = (GameObject) GameObject.Instantiate(this.GObuttonX);
                tmpButton = tmpGO.GetComponent<Button>();
                break;
            case button_Y:
                tmpGO = (GameObject) GameObject.Instantiate(this.GObuttonY);
                tmpButton = tmpGO.GetComponent<Button>();
                break;
        }
        tmpGO.transform.SetParent(this.buttonsLine.transform);
        tmpButton.image.enabled = true;
        this.buttonsProcessed.Add(tmpButton);
    }

    void ResetMinigame()
    {
        this.buttonHitCount = 0;
        this.timeElapsed = 0.0f;
        this.minigamePaused = false;
        timeIntervalElapsed = this.timeInterval / 2;
        for (int i = 0; i < this.buttonsProcessed.Count; i++)
        {
            if(this.buttonsProcessed[i] != null)
            {
                GameObject.Destroy(this.buttonsProcessed[i].gameObject);
            }
        }
        this.buttonsProcessed.Clear();
    }

    void OnEnable()
    {
        this.OnMinigameWin += SelfDisable;
        this.OnMinigameLost += SelfDisable;
        this.OnGuitarButtonHit += Hit;
        this.OnGuitarButtonMiss += Miss;
        this.OnButtonHitCountValueChanged += HitCountHandler;
        ResetMinigame();
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
        this.OnMinigameLost -= SelfDisable;
        this.OnGuitarButtonHit -= Hit;
        this.OnGuitarButtonMiss -= Miss;
        this.OnButtonHitCountValueChanged -= HitCountHandler;
    }

    void Hit()
    {
        this.buttonHitCount++;
        Debug.Log("hit");
        this.areEffectsOn = true;
        this.effectTimeElapsed = 0.0f;

        Color tmpColor = this.backgroundImage.color;
        float alpha = tmpColor.a;
        tmpColor = Color.green;
        tmpColor.a = alpha;
        this.backgroundImage.color = tmpColor;

        tmpColor = this.frameImage.color;
        alpha = tmpColor.a;
        tmpColor = Color.green;
        tmpColor.a = alpha;
        tmpColor *= 0.8f;
        this.frameImage.color = tmpColor;
    }
    void Miss()
    {
        OnMinigameLost();
        Debug.Log("Miss");

        this.areEffectsOn = true;
        this.effectTimeElapsed = 0.0f;

        Color tmpColor = this.backgroundImage.color;
        float alpha = tmpColor.a;
        tmpColor = Color.red;
        tmpColor.a = alpha;
        this.backgroundImage.color = tmpColor;
        tmpColor = this.frameImage.color;
        alpha = tmpColor.a;
        tmpColor = Color.red;
        tmpColor.a = alpha;
        tmpColor *= 0.8f;
        this.frameImage.color = tmpColor;
    }

    void HitCountHandler(int value)
    {
        this.hitCountText.text = "" + value + " / " + this.hitCountTarget;
    }
}
