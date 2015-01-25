using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{
    public PowerUpManager.PowerUpType type = PowerUpManager.PowerUpType.PU_NONE;

    private bool inContanct = false;

	void Start () 
    {
	
	}
	void Update () 
    {
	    if(inContanct)
        {
            if(Input.GetAxis("Fire1") >= 0.05f)
            {
                if(!PowerUpManager.IsBoostOn)
                {
                    PowerUpManager.PickUp(this.type);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.inContanct = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.inContanct = false;
        }
    }
}
