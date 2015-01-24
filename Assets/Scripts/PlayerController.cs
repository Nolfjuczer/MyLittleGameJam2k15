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
        this.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized * Time.deltaTime * MovementSpeed;
	}
}
