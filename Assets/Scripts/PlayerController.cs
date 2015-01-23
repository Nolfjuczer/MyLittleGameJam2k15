using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Vertical")>0.0f)
        {
            this.transform.position += new Vector3(0.0f, 0.05f, 0.0f);
        }
        if(Input.GetAxis("Vertical")<0.0f)
        {
            this.transform.position += new Vector3(0.0f, -0.05f, 0.0f);
        }
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            this.transform.position += new Vector3(0.05f, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            this.transform.position += new Vector3(-0.05f, 0.0f, 0.0f);
        }

	
	}
}
