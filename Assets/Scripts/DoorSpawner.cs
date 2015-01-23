using UnityEngine;
using System.Collections;

public class DoorSpawner : MonoBehaviour {


    public Sprite Doors;

    public GameObject[] doors1 = new GameObject[2];
    public GameObject[] doors2 = new GameObject[2];
    public GameObject[] doors3 = new GameObject[2];

	// Use this for initialization
	void Start () {
        
        int rand = Random.Range(0, 1);
        doors1[rand].gameObject.GetComponent<SpriteRenderer>().sprite = Doors;
        rand = Random.Range(0, 1);
        doors2[rand].gameObject.GetComponent<SpriteRenderer>().sprite = Doors;
        rand = Random.Range(0, 1);
        doors3[rand].gameObject.GetComponent<SpriteRenderer>().sprite = Doors;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
