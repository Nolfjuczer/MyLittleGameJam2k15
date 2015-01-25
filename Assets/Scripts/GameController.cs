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
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = new GameController();
        DeviceController = new DeviceController();
        FixedItems = 0;
        DevicesCounter = 0;
        GameOver = false;
        DevicesState2Over = false;
        DevicesState1Over = false;
        GameState = GameStateEnum.firstStage;
    }

    void Update()
    {
        if (!GameOver)
        {
            int counter = 0;
            if (GameController.Instance.GameState == GameStateEnum.thirdStage)
            {
                foreach (GameObject device in DeviceController.DevicesStage3)
                {
                    if (!device.GetComponent<Device>().IsWorking) counter += 1;
                }
                if (counter == DevicesCounter)
                {
                    GameOver = true;
                    GameOverMenu.SetActive(true);
                }
            }
            else if (GameController.Instance.GameState == GameStateEnum.secondStage)
            {
                foreach (GameObject device in DeviceController.DevicesStage2)
                {
                    if (!device.GetComponent<Device>().IsWorking) counter += 1;

                }
                if (counter == 8)
                {
                    DevicesState2Over = true;
                }
            }
            else if (GameController.Instance.GameState == GameStateEnum.firstStage)
            {
                foreach (GameObject device in DeviceController.DevicesStage1)
                {
                    if (!device.GetComponent<Device>().IsWorking) counter += 1;
                }
                if (counter == 2)
                {
                    DevicesState1Over = true;
                }
            }
            if (counter == DevicesCounter)
            {
                GameOver = true;
                GameOverMenu.SetActive(true);
            }
        }
    }

    public DeviceController DeviceController;
    public int FixedItems;
    public int DevicesCounter;
    public bool GameOver = false;
    public bool DevicesState2Over = false;
    public bool DevicesState1Over = false;
    public GameStateEnum GameState = GameStateEnum.firstStage;


    public void PlayAgain()
    {
        GameState = GameStateEnum.firstStage;
        DeviceController.Stage1Started();
        Application.LoadLevel(Application.loadedLevelName);
        DeviceController.Stage1Started();
    }
    public void GoToMenu()
    {
        Application.LoadLevel("menu");
    }
    public void FixAffect()
    {
        this.FixedItems += 1;
        if (FixedItems == 2)
        {
            GameState = GameStateEnum.secondStage;
            DeviceController.Stage2Started();
        }
        if (FixedItems == 6)
        {
            GameState = GameStateEnum.thirdStage;
            DeviceController.Stage3Started();
        }

    }
}

public enum GameStateEnum
{
    firstStage = 1,
    secondStage = 2,
    thirdStage = 3,
}
