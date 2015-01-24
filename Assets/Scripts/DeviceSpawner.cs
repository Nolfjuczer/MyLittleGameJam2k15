using UnityEngine;
using System.Collections;

public class DeviceSpawner : MonoBehaviour {

    public GameObject[] Slots = new GameObject[3];

    private GameObject device1;
    private GameObject device2;

	// Use this for initialization

    void Awake()
    {
        device1 = transform.parent.GetComponentInChildren<RoomDeviceController>().Device1;
        device2 = transform.parent.GetComponentInChildren<RoomDeviceController>().Device2;
        int rand1 = Random.Range(0, 3);
        GameObject dev1 = (GameObject)Instantiate(device1, Slots[rand1].gameObject.transform.position, Slots[rand1].gameObject.transform.rotation);
        DeviceController.Devices.Add(dev1);
        GameController.Instance.DevicesCounter += 1;
        int rand2 = rand1;
        while (rand2 == rand1) rand2 = Random.Range(0, 3);
        GameObject dev2 = (GameObject)Instantiate(device2, Slots[rand2].gameObject.transform.position, Slots[rand2].gameObject.transform.rotation);
        DeviceController.Devices.Add(dev2);
        GameController.Instance.DevicesCounter += 1;
    }
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
