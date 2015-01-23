using UnityEngine;
using System.Collections;

public class DeviceSpawner : MonoBehaviour {

    public GameObject Device1;
    public GameObject Device2;
    public GameObject[] Slots = new GameObject[3];
    
	// Use this for initialization
	void Start () {
        int rand1 = Random.Range(0, 3);
        Instantiate(Device1, Slots[rand1].gameObject.transform.position, Slots[rand1].gameObject.transform.rotation);
        int rand2 = rand1;
        while(rand2==rand1)rand2 = Random.Range(0, 3);
        Instantiate(Device1, Slots[rand2].gameObject.transform.position, Slots[rand2].gameObject.transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
