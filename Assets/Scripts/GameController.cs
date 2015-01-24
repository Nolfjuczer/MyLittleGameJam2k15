using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController _instance;
    public GameObject GameOverMenu = null;

    public static GameController Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this !=_instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        if (!GameOver)
        {
            int counter = 0;
            foreach (GameObject device in DeviceController.Devices)
            {
                if (!device.GetComponent<Device>().IsWorking) counter += 1;
            }
            if (counter == DevicesCounter)
            {
                GameOver = true;
                GameOverMenu.SetActive(true);
            }
            if (GameOver)
            {
                //Debug.Log("Przegrałeś");
                //Debug.Break();
            }
        }
    }

    public DeviceController DeviceController;
    public int FixedItems;
    public int DevicesCounter;
    public bool GameOver = false;

    public void PlayAgain()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
    public void GoToMenu()
    {
        Application.LoadLevel("menu");
    }
}
