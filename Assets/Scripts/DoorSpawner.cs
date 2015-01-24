using UnityEngine;
using System.Collections;

public class DoorSpawner : MonoBehaviour {


    public Sprite Doors;
    public Sprite DoorsTurned;

    public GameObject[] doors1 = new GameObject[2];
    public GameObject[] doors2 = new GameObject[2];
    public GameObject[] doors3 = new GameObject[2];

	// Use this for initialization
	void Start () {
        GameObject tmp = new GameObject();
        int rand = Random.Range(0, 2);
        tmp = doors1[rand].gameObject;
        if(tmp.transform.rotation.z != 0.0f) tmp.GetComponent<SpriteRenderer>().sprite = Doors;
        else tmp.GetComponent<SpriteRenderer>().sprite = DoorsTurned;
        tmp.GetComponent<BoxCollider2D>().enabled = false;
        rand = Random.Range(0, 2);
        tmp = doors2[rand].gameObject;
        if (tmp.transform.rotation.z !=0.0f) tmp.GetComponent<SpriteRenderer>().sprite = Doors;
        else tmp.GetComponent<SpriteRenderer>().sprite = DoorsTurned;
        tmp.GetComponent<BoxCollider2D>().enabled = false;
        rand = Random.Range(0, 2);
        tmp = doors3[rand].gameObject;
        if (tmp.transform.rotation.z != 0.0f) tmp.GetComponent<SpriteRenderer>().sprite = Doors;
        else tmp.GetComponent<SpriteRenderer>().sprite = DoorsTurned;
        tmp.GetComponent<BoxCollider2D>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
