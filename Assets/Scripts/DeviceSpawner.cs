using UnityEngine;
using System.Collections;

public class DeviceSpawner : MonoBehaviour {

    public GameObject[] Slots = new GameObject[3];



	// Use this for initialization

    void Awake()
    {
        GameObject device1;
        GameObject device2;
        device1 = transform.GetComponentInChildren<RoomDeviceController>().Device1;
        device2 = transform.GetComponentInChildren<RoomDeviceController>().Device2;
        int rand1 = Random.Range(0, 3);
        GameObject dev1 = (GameObject)Instantiate(device1, Slots[rand1].gameObject.transform.position, Slots[rand1].gameObject.transform.rotation);
        DeviceController.DevicesStage3.Add(dev1);
        GameController.Instance.DevicesCounter += 1;
        int rand2 = rand1;
        while (rand2 == rand1) rand2 = Random.Range(0, 3);
        GameObject dev2 = (GameObject)Instantiate(device2, Slots[rand2].gameObject.transform.position, Slots[rand2].gameObject.transform.rotation);
        DeviceController.DevicesStage3.Add(dev2);
        GameController.Instance.DevicesCounter += 1;
        if(this.gameObject.name == "Room5")
        {
            DeviceController.DevicesStage1.Add(dev1);
            DeviceController.DevicesStage1.Add(dev2);
        }
        if(this.gameObject.name == "Room5" || this.gameObject.name == "Room6" || this.gameObject.name == "Room8" || this.gameObject.name == "Room9")
        {
            DeviceController.DevicesStage2.Add(dev1);
            DeviceController.DevicesStage2.Add(dev2);
        }
    }
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
