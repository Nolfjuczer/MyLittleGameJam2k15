using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeviceController : MonoBehaviour {

    public static List<GameObject> DevicesStage3 = new List<GameObject>();
    public static List<GameObject> DevicesStage1 = new List<GameObject>();
    public static List<GameObject> DevicesStage2 = new List<GameObject>();

    private float timeWhenDestroyed;
    private float timeDestroyPeriod;


	void Start () 
    {
        int rand = Random.Range(0, 1);
        DevicesStage1[rand].GetComponent<Device>().DestroyDevice();
        timeDestroyPeriod = 20.0f;
        timeWhenDestroyed = 10.0f;
 
	}
	
	void Update () 
    {
        int tmpUpdate = 0;
        if (!GameController.Instance.GameOver)
        {
            if (GameController.Instance.GameState == GameStateEnum.thirdStage)
            {
                tmpUpdate = 0;
                if (timeDestroyPeriod > 10.0f) timeDestroyPeriod -= Time.deltaTime / 5.0f;
                if (Time.time - timeWhenDestroyed > timeDestroyPeriod)
                {
                    do
                    {
                        tmpUpdate = Random.Range(0, DevicesStage3.Count);
                    } while (!DevicesStage3[tmpUpdate].GetComponent<Device>().IsWorking);
                    timeWhenDestroyed = Time.time;
                    DevicesStage3[tmpUpdate].GetComponent<Device>().DestroyDevice();
                }
            }
        }
	}

    public void UsedJoint()
    {
        timeWhenDestroyed = Time.time;
    }

    public void SomethingFixed()
    {
        if (!GameController.Instance.GameOver)
        {
            if (GameController.Instance.GameState == GameStateEnum.thirdStage)
            {
                int rand = Random.Range(0, 4);
                switch (rand)
                {
                    case 0:
                    {
                        break;
                    }
                    case 3:
                    {
                        break;
                    }
                    case 1:
                    {
                        int tmp = 0;
                        do
                        {
                            tmp = Random.Range(0, DevicesStage3.Count);
                        } while (!DevicesStage3[tmp].GetComponent<Device>().IsWorking);
                        DevicesStage3[tmp].GetComponent<Device>().DestroyDevice();
                        break;
                        }
                    case 2:
                    {
                        int tmp = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            do
                            {
                                tmp = Random.Range(0, DevicesStage3.Count);
                            } while (!DevicesStage3[tmp].GetComponent<Device>().IsWorking);
                            DevicesStage3[tmp].GetComponent<Device>().DestroyDevice();
                        }
                        break;
                    }
                }
            }

            else if (GameController.Instance.GameState == GameStateEnum.secondStage && !GameController.Instance.DevicesState2Over)
            {
                int rand = Random.Range(1, 2);
                switch (rand)
                {
                    case 1:
                        {
                            int tmp = 0;
                            do
                            {
                                tmp = Random.Range(0, DevicesStage2.Count);
                            } while (!DevicesStage2[tmp].GetComponent<Device>().IsWorking);
                            DevicesStage2[tmp].GetComponent<Device>().DestroyDevice();
                            break;
                        }
                    case 2:
                        {
                            int tmp = 0;
                            for (int i = 0; i < 2; i++)
                            {
                                do
                                {
                                    tmp = Random.Range(0, DevicesStage2.Count);
                                } while (!DevicesStage2[tmp].GetComponent<Device>().IsWorking);
                                DevicesStage2[tmp].GetComponent<Device>().DestroyDevice();
                            }
                            break;
                        }
                }
            }

            else if (GameController.Instance.GameState == GameStateEnum.firstStage && !GameController.Instance.DevicesState1Over)
            {
                int tmp = 0;
                do
                {
                    tmp = Random.Range(0, DevicesStage1.Count);
                } while (!DevicesStage1[tmp].GetComponent<Device>().IsWorking);
                DevicesStage1[tmp].GetComponent<Device>().DestroyDevice();
            }
        }
    }

    public void SomethingNotFixed()
    {
        if(!GameController.Instance.GameOver )
        {
            if (GameController.Instance.GameState == GameStateEnum.thirdStage)
            {
                int tmp = 0;
                do
                {
                    tmp = Random.Range(0, DevicesStage3.Count);
                } while (!DevicesStage3[tmp].GetComponent<Device>().IsWorking);
                DevicesStage3[tmp].GetComponent<Device>().DestroyDevice();
            }

            else if (GameController.Instance.GameState == GameStateEnum.secondStage && !GameController.Instance.DevicesState2Over)
            {
                int tmp = 0;
                do
                {
                    tmp = Random.Range(0, DevicesStage2.Count);
                } while (!DevicesStage2[tmp].GetComponent<Device>().IsWorking);
                DevicesStage2[tmp].GetComponent<Device>().DestroyDevice();
            }

            else if (GameController.Instance.GameState == GameStateEnum.firstStage && !GameController.Instance.DevicesState1Over)
            {
                int tmp = 0;
                do
                {
                    tmp = Random.Range(0, DevicesStage1.Count);
                } while (!DevicesStage1[tmp].GetComponent<Device>().IsWorking);
                DevicesStage1[tmp].GetComponent<Device>().DestroyDevice();
            }
        }
    }

    public void Stage1Started()
    {
        int rand = Random.Range(0, 1);
        DevicesStage1[rand].GetComponent<Device>().DestroyDevice();
    }

    public void Stage2Started()
    {
        if (DevicesStage2.Count != 8) Debug.Break();
        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, DevicesStage2.Count);
            DevicesStage2[rand].GetComponent<Device>().DestroyDevice();
        }

    }
    public void Stage3Started()
    {
        if (DevicesStage3.Count != GameController.Instance.DevicesCounter) Debug.Break();
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, DevicesStage3.Count);
            DevicesStage3[rand].GetComponent<Device>().DestroyDevice();
        }
    }
}
