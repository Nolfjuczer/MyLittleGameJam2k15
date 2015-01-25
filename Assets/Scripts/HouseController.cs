using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour
{

    public GameObject[] Rooms = new GameObject[9];
    public GameObject[] RoomsSlots = new GameObject[9];

    private List<GameObject> roomsSlots = new List<GameObject>();

    void Start()
    {
        int[] tab = { -1, -2, -3, -4, -5, -6, -7, -8, -9 };
        bool contains = true;
        for (int j = 0; j < 9; j++)
        {
            roomsSlots.Add(RoomsSlots[j]);
        }
        for (int i = 0; i < 9; i++)
        {
            int rand = -1;
            do
            {
                rand = Random.Range(0,9);
                foreach(int x in tab)
                {
                    if (x == rand)
                    {
                        contains = true;
                        break;
                    }
                    else
                    {
                        contains = false;
                    }
                }                               
            }while(contains);
            tab[i]=rand;
            Rooms[i].transform.position = RoomsSlots[tab[i]].transform.position;
        }

    }
}
