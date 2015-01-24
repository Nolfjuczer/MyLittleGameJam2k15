using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    public ParticleSystem[] Particles = new ParticleSystem[2];

    private bool isWorking;

    private float timerWhenCollided;

    public bool IsWorking
    {
        get
        {
            return isWorking;
        }
        set
        {
            isWorking = value;
        }
    }

    void Awake()
    {
        isWorking = true;
        for(int i =0;i<Particles.Length;i++)
        {
            Particles[i].enableEmission = false;
        }
        
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    public void FixDevice()
    {
        for (int i = 0; i < Particles.Length; i++)
        {
            Particles[i].enableEmission = false;
        }
        isWorking = true;
    }

    public void DestroyDevice()
    {
        for (int i = 0; i < Particles.Length; i++)
        {
            Particles[i].enableEmission = true;
        }
        isWorking = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !isWorking && Time.time - timerWhenCollided > 2.0f)
        {
            timerWhenCollided = Time.time;
            MinigameManager.LaunchMinigame();
            MinigameManager.OnMinigameWin += Win;
            MinigameManager.OnMinigameLost += Lose;
        }
    }

    void Win()
    {
        GameController.Instance.FixedItems += 1;
        GameController.Instance.DeviceController.SomethingFixed();
        MinigameManager.OnMinigameWin -= Win;
        MinigameManager.OnMinigameLost -= Lose;
        FixDevice();

    }

    void Lose()
    {
        GameController.Instance.DeviceController.SomethingNotFixed();
        MinigameManager.OnMinigameWin -= Win;
        MinigameManager.OnMinigameLost -= Lose;
        DestroyDevice();
    }
}
