using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public float buttonsSpeed = 0.75f;

    public Vector3 StartPos = Vector3.zero;
    public Vector3 EndPos = Vector3.zero;

    public Image backgroundImage = null;

    private List<Button> buttonsProcessed = new List<Button>();

    [Header("Params")]
    [Space(10)]
    public int buttonPressAmount = 4;
    public int buttonPressCount = 0;
    private float timeElapsed = 0.0f;
    private float timeIntervalElapsed = 0.0f;
    private float timeInterval = 0.75f;
    
    void Start () 
    {
        
	}
	void Update () 
    {
        this.timeElapsed += Time.deltaTime;
        this.timeIntervalElapsed += Time.deltaTime;

        if (this.buttonsProcessed != null)
        {
            for (int i = 0; i < this.buttonsProcessed.Count; i++)
            {
                this.buttonsProcessed[i].progres += Time.deltaTime * buttonsSpeed;

                Vector3 pos = this.buttonsProcessed[i].rectTransform.localPosition;
                pos = Vector3.Lerp(this.StartPos, EndPos, this.buttonsProcessed[i].progres);
                this.buttonsProcessed[i].rectTransform.localPosition = pos;

                Color tmpColor = this.buttonsProcessed[i].image.color;
                //tmpColor.a = 
            }
        }

        if(this.timeIntervalElapsed >= this.timeInterval)
        {
            this.timeIntervalElapsed -= this.timeInterval;
            AddButton();
        }
        for (int i = 0; i < this.buttonsProcessed.Count; i++)
        {
            if(this.buttonsProcessed[i].progres >= 1.0f)
            {
                GameObject.Destroy(this.buttonsProcessed[i].gameObject);
                this.buttonsProcessed.RemoveAt(i);
                i--;
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
        this.buttonPressCount = 0;
        this.timeElapsed = 0.0f;
        timeIntervalElapsed = 0.0f;
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
        ResetMinigame();
        this.OnMinigameWin += SelfDisable;
        this.OnMinigameLost += SelfDisable;
    }
    void OnDisable()
    {
        this.OnMinigameWin -= SelfDisable;
        this.OnMinigameLost -= SelfDisable;
    }
}
