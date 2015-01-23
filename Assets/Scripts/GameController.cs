using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController _instance;

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

    public int FixedItems;

}
