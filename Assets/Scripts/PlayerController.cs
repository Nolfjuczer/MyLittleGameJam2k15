using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(1.0f,10.0f)]
    public float MovementSpeed;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!MinigameManager.IsMinigamePlaying && !GameController.Instance.GameOver)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            this.transform.position += input.normalized * Time.deltaTime * MovementSpeed * input.magnitude * PowerUpManager.PillsSpeedMultiplier;
        }
	}
}
