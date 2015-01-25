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
    
    private void SpawnPowerUp()
    {
        int rand = Random.Range(0, 3);
        switch(rand)
        {
            case 1:
            {
                GameObject pUP = PowerUpManager.GetNewPowerUp();
                pUP.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.5f) +(this.transform.up * -1.28f);
                break;
            }
            default:
            {
                break;
            }
        }
    }

    public void FixDevice()
    {
        for (int i = 0; i < Particles.Length; i++)
        {
            Particles[i].enableEmission = false;
        }
        isWorking = true;
        SpawnPowerUp();
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
        if (col.gameObject.tag == "Player" && !isWorking && Time.time - timerWhenCollided > 1.0f)
        {
            timerWhenCollided = Time.time;
            MinigameManager.LaunchMinigame();
            MinigameManager.OnMinigameWin += Win;
            MinigameManager.OnMinigameLost += Lose;
        }
    }

    void Win()
    {
        GameController.Instance.FixAffect();
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
