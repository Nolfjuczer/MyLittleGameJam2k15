using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
    public Speaker speakerGO = null;
    public Speaker musicSpeaker = null;

    private static SoundManager entity = null;
    public AudioClip[] sounds = null;
    private List<Speaker> speakers = new List<Speaker>();

    private GameStateEnum lastGameState = GameStateEnum.thirdStage;
    private float musicVolume = 0.5f;

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
        if (PowerUpManager.IsBoostOn)
        {
            this.musicSpeaker.Volume = 0.0f;
        }
        else
        {
            this.musicSpeaker.Volume = this.musicVolume;
        }

        GameStateEnum state = GameController.Instance.GameState;
        if (state != this.lastGameState)
        {
            switch (state)
            {
                case GameStateEnum.firstStage:
                    musicSpeaker.PlaySound(GetClip(getMusicName(state)), true, this.musicVolume);
                    //musicSpeaker.FadeOutSound(getMusicName(lastGameState));
                    break;
                case GameStateEnum.secondStage:
                    musicSpeaker.PlaySound(GetClip(getMusicName(state)), true, this.musicVolume);
                    //musicSpeaker.FadeOutSound(getMusicName(lastGameState));
                    break;
                case GameStateEnum.thirdStage:
                    musicSpeaker.PlaySound(GetClip(getMusicName(state)), true, this.musicVolume);
                    //musicSpeaker.FadeOutSound(getMusicName(lastGameState));
                    break;
            }
        }
        this.lastGameState = state;
	}

    string getMusicName(GameStateEnum state)
    {
        switch(state)
        {
            case GameStateEnum.firstStage:
                return "1";
                break;
            case GameStateEnum.secondStage:
                return "2";
                break;
            case GameStateEnum.thirdStage:
                return "3";
                break;
        }
        return "";
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
        this.speakers.Add(tmpSpeaker);
        return tmpSpeaker;
    }

    AudioClip GetClip(string clipName)
    {
        AudioClip clip = null;
        for (int i = 0; i < SoundManager.entity.sounds.Length; i++)
        {
            if (SoundManager.entity.sounds[i] != null)
            {
                if (SoundManager.entity.sounds[i].name == clipName)
                {
                    clip = SoundManager.entity.sounds[i];
                }
            }
        }
        return clip;
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

    public static void StopSound(string name)
    {
        for(int i = 0;i < SoundManager.entity.speakers.Count;i++)
        {
            if(SoundManager.entity.speakers[i] != null)
            {
                if(SoundManager.entity.speakers[i].name == name)
                {
                    SoundManager.entity.speakers[i].StopSound();
                }
            }
        }
    }

    public static void FadeOutSound(string name, float time = 0.5f)
    {
        for (int i = 0; i < SoundManager.entity.speakers.Count; i++)
        {
            if (SoundManager.entity.speakers[i] != null)
            {
                if (SoundManager.entity.speakers[i].ClipName == name)
                {
                    SoundManager.entity.speakers[i].Fadeout(time);
                }
            }
        }
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
