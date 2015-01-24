using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeviceController : MonoBehaviour {

    public static List<GameObject> Devices = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //int rand = Random.Range(0, 18);
        if (Devices.Count != GameController.Instance.DevicesCounter) Debug.Break();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
