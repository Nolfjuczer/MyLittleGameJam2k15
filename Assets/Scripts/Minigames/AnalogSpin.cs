using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnalogSpin : Minigame
{
    #region Variables

    public const int DIR_CENTER = 5;
    public const int DIR_RT = 0;
    public const int DIR_LT = 1;
    public const int DIR_LB = 2;
    public const int DIR_RB = 3;

    public const int ROT_DIR_LEFT = 0;
    public const int ROT_DIR_RIGHT = 1;

    public Image AnalogPointerImage = null;
    public Image BackgroundImage = null;
    public RectTransform AnalogPointerRectTransform = null;
    public Slider analogTimeSlider = null;
    public Text counter = null;

    public Image arrowRight = null;
    public Image arrowLeft = null;

    private Color color_default = new Color(1.0f,1.0f,1.0f,0.4f);
    private Color color_win = new Color(0.0f, 1.0f, 0.0f, 0.4f);
    private Color color_lost = new Color(1.0f, 0.0f, 0.0f, 0.4f);

    [Header("Parameters")]
    [Space(10)]
    public float timeLength = 5.0f;
    private float timeElapsed = 0.0f;
    public float timeMultiplier = 0.9f;
    public int SpinTarget = 5;
    public int SpintTargetIncrease = 1;
    private int SpinCount = 0;

    private int lastDir = -1;

    private int rotateRightCounter = 0;
    private int rotateLeftCounter = 0;

    private int currentRotation
    {
        get
        {
            return this.rotateRightCounter - this.rotateLeftCounter;
        }
    }

    private int lastCurrentRot = 0;
    

    private bool minigamPaused = false;

    private int direction = -1;

    #endregion


    #region Monobehaviour Methods
    void Start () 
    {
	
	}
	void Update () 
    {
	    if(!this.minigamPaused)
        {
            this.timeElapsed += Time.deltaTime;
            //Input.GetAxis("Vertical2")
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0.0f).normalized;
            Vector3 pos = dir * 80.0f;
            this.AnalogPointerRectTransform.localPosition = pos;

            int currentDir = GetQuadrant(dir);
            if(currentDir == AnalogSpin.DIR_CENTER)
            {
                this.rotateLeftCounter = 0;
                this.rotateRightCounter = 0;
            }
            if(myMod(currentDir - 1,4) == lastDir )
            {
                this.rotateLeftCounter++;
            }
            if (myMod(currentDir + 1, 4) == lastDir)
            {
                this.rotateRightCounter++;
            }
            lastDir = currentDir;
            
            if(this.direction == ROT_DIR_RIGHT)
            {
                if(this.currentRotation / 4 >= this.SpinTarget)
                {
                    NotifyOnMinigameWin();
                    this.BackgroundImage.color = this.color_win;
                    this.minigamPaused = true;
                    //IncreaseDiff();
                }
            }
            else
            {
                if (-this.currentRotation / 4 >= this.SpinTarget)
                {
                    NotifyOnMinigameWin();
                    this.BackgroundImage.color = this.color_win;
                    this.minigamPaused = true;
                    //IncreaseDiff();
                }
            }

            this.analogTimeSlider.value = this.timeElapsed / this.timeLength;
            if(this.timeElapsed >= this.timeLength)
            {
                NotifyOnMinigameLost();
                this.BackgroundImage.color = this.color_lost;
                this.minigamPaused = true;
                IncreaseDiff();
            }


            int currentRot = this.currentRotation / 4;
            if(currentRot != this.lastCurrentRot)
            {
                this.counter.text = "" + ((this.direction == ROT_DIR_RIGHT) ? currentRot : -currentRot).ToString() + " / " + this.SpinTarget;
            }
            this.lastCurrentRot = currentRot;
        }
	}

    int myMod (int a, int b)
    {
        return a >= 0 ? a % b : (a % b) + b;
    }

    int GetQuadrant(Vector3 dir)
    {
        if(dir == Vector3.zero)
        {
            return AnalogSpin.DIR_CENTER;
        }
        if(dir.x > 0.0)
        {
            if(dir.y > 0.0)
            {
                return AnalogSpin.DIR_RT;
            }
            else
            {
                return AnalogSpin.DIR_RB;
            }
        }
        else
        {
            if (dir.y > 0.0)
            {
                return AnalogSpin.DIR_LT;
            }
            else
            {
                return AnalogSpin.DIR_LB;
            }
        }
    }

    void ResetMinigame()
    {
        this.minigamPaused = false;
        this.rotateRightCounter = 0;
        this.rotateLeftCounter = 0;
        this.timeElapsed = 0.0f;
        this.lastCurrentRot = -5000;
        this.BackgroundImage.color = this.color_default;
        this.analogTimeSlider.value = 0.0f;
        System.Random ran = new System.Random();
        this.direction = ran.Next(0, 2);

        if (this.arrowLeft != null && this.arrowRight != null)
        {
            this.arrowRight.enabled = false;
            this.arrowLeft.enabled = false;
            if (this.direction == ROT_DIR_RIGHT)
            {
                this.arrowRight.enabled = true;
            }
            else
            {
                this.arrowLeft.enabled = true;
            }
        }
    }
    void IncreaseDiff()
    {
        this.SpinTarget += this.SpintTargetIncrease;
        this.timeLength *= this.timeMultiplier;
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
}
