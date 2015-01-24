using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeviceController : MonoBehaviour {

    public static List<GameObject> Devices = new List<GameObject>();

    private float timeWhenDestroyed;
    private float timeDestroyPeriod;
    private int rand = 0;
    private int tmpUpdate = 0;

	// Use this for initialization
	void Start () {
        if (Devices.Count != GameController.Instance.DevicesCounter) Debug.Break();
        for(int i = 0; i<3 ; i++)
        {
            rand = Random.Range(0, Devices.Count);
            Devices[rand].GetComponent<Device>().DestroyDevice(); ;
        }
        timeDestroyPeriod = 20.0f;
        timeWhenDestroyed = 30.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("DestroyPeriod= " + timeDestroyPeriod);
        if (timeDestroyPeriod > 10.0f) timeDestroyPeriod -= Time.deltaTime / 10.0f;
        if(Time.time - timeWhenDestroyed > timeDestroyPeriod)
        {
            do
            {
                tmpUpdate = Random.Range(0, Devices.Count);
            } while (Devices[tmpUpdate].GetComponent<Device>().IsWorking);
            timeWhenDestroyed = Time.time;
            Devices[tmpUpdate].GetComponent<Device>().DestroyDevice();
        }
	    
	}


    public void SomethingFixed()
    {
        rand = Random.Range(0, 2);
        switch(rand)
        {
            case 0:
            {
                break;
            }
            case 1:
            {
                int tmp = 0;
                do
                {
                    tmp = Random.Range(0, Devices.Count); 
                } while (Devices[tmp].GetComponent<Device>().IsWorking);
                Devices[tmp].GetComponent<Device>().DestroyDevice();
                break;
            }
            case 2:
            {
                int tmp = 0;
                for (int i = 0; i < 2; i++)
                {
                    do
                    {
                        tmp = Random.Range(0, Devices.Count);
                    } while (Devices[tmp].GetComponent<Device>().IsWorking);
                    Devices[tmp].GetComponent<Device>().DestroyDevice();
                }
                break;
            }
        }
    }

    public void SomethingNotFixed()
    {
        int tmp = 0;
        do
        {
            tmp = Random.Range(0, Devices.Count);
        } while (Devices[tmp].GetComponent<Device>().IsWorking);
        Devices[tmp].GetComponent<Device>().DestroyDevice() ;
    }
}
