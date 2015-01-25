using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(1.0f,10.0f)]
    public float MovementSpeed;

    public GameObject pillsParticles = null;
    public ParticleSystem pillsParticleSystem = null;

    public Vector3 targetDir;

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

            if (PowerUpManager.IsPilled)
            {
                this.pillsParticleSystem.enableEmission = true;
                if (this.pillsParticles != null)
                {
                    Vector3 targetLookAt = this.pillsParticles.transform.position - input.normalized;
                    this.pillsParticles.transform.LookAt(targetLookAt);
                    targetDir = input.normalized;
                }
                if (this.pillsParticleSystem != null)
                {
                    this.pillsParticleSystem.startRotation = -Mathf.Atan(input.y / input.x);
                }
            }
            else
            {
                this.pillsParticleSystem.enableEmission = false;
            }
        }
	}
}
