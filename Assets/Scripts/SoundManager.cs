using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
    public Speaker speakerGO = null;

    private static SoundManager entity = null;
    public AudioClip[] sounds = null;
    private List<Speaker> speakers = new List<Speaker>();

	void Start () 
    {
        SoundManager.entity = this;
        for(int i = 0;i < this.sounds.Length;i++)
        {
            Debug.Log(sounds[i].name);
        }
	}
	void Update () 
    {
	
	}

    void OnEnable()
    {
        MinigameManager.OnMinigameWin += EventWin;
        MinigameManager.OnMinigameLost += EventLost;
    }
    void OnDisable()
    {
        MinigameManager.OnMinigameWin -= EventWin;
        MinigameManager.OnMinigameLost -= EventLost;
    }

    Speaker GetSpeaker()
    {
        Speaker tmpSpeaker = null;
        if(this.speakers != null)
        {
            for (int i = 0; i < this.speakers.Count; i++)
            {
                if(!this.speakers[i].IsPlaying)
                {
                    tmpSpeaker = this.speakers[i];
                    tmpSpeaker.gameObject.SetActive(true);
                    return tmpSpeaker;
                }
            }
        }
        GameObject tmpGO = (GameObject)GameObject.Instantiate(this.speakerGO.gameObject);
        tmpGO.transform.parent = this.transform;
        tmpGO.transform.position = Vector3.zero;
        tmpSpeaker = tmpGO.GetComponent<Speaker>();
        return tmpSpeaker;
    }

    public static void PlaySound(string name,bool loop = false,float volume = 1.0f)
    {
        Speaker tmpSpeaker = SoundManager.entity.GetSpeaker();
        AudioClip clip = null;
        for (int i = 0; i < SoundManager.entity.sounds.Length; i++)
        {
            if(SoundManager.entity.sounds[i] != null)
            { 
                if(SoundManager.entity.sounds[i].name == name)
                {
                    clip = SoundManager.entity.sounds[i];
                }
            }
        }
        if(clip != null)
        {
            tmpSpeaker.PlaySound(clip, loop, volume);
        }   
        //tmpSpeaker.PlaySound()
    }
    public static void PlaySound(int index, bool loop = false, float volume = 1.0f)
    {
        Speaker tmpSpeaker = SoundManager.entity.GetSpeaker();
        AudioClip clip = null;
        if(index >= 0 && index < SoundManager.entity.sounds.Length)
        {
            tmpSpeaker.PlaySound(clip, loop, volume);
        }
        //tmpSpeaker.PlaySound()
    }

    void EventWin()
    {
        SoundManager.PlaySound("quicktime_event_win");
    }
    void EventLost()
    {
        SoundManager.PlaySound("quicktime_event_fail");

    }
}
