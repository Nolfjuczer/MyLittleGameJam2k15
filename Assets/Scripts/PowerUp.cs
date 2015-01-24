using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{
    public PowerUpManager.PowerUpType type = PowerUpManager.PowerUpType.PU_NONE;
	void Start () 
    {
	
	}
	void Update () 
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Input.GetAxis("Fire1") >= 0.05f)
        {
            PowerUpManager.PickUp(this.type);
        }
    }
}
