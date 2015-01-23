using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public Text Timer;
    public Text FixedItems;

    private int secs;
    private int mins;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        secs = (int)Time.time % 60;
        mins = (int)Time.time / 60;
        Timer.text = string.Format("{0:00}:{1:00}", mins, secs);
        FixedItems.text = "Fixed: " + GameController.Instance.FixedItems;
	}
}
