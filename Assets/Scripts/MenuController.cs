using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
    public string mainLevelName = "Scene test";
	void Start () 
    {
	
	}
	void Update () 
    {
	
	}

    public void StartGame()
    {
        Application.LoadLevel(mainLevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
