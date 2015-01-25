using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour 
{
    public AudioSource source = null;
    public AudioClip clip = null;

    private float audioLength = 0.0f;
    private float audioLengthLeft = 0.0f;
    private bool loop = false;
    private bool isFadeOut = false;

    private float timeElapsedFadeOut = 0.0f;
    private float timeLengthFadeOut = 0.0f;

    private float startVolume = 1.0f;

    public string ClipName
    {
        get
        {
            return this.clip.name;
        }
    }

    public bool IsPlaying
    {
        get
        {
            return this.source.isPlaying;
        }
    }
	void Start () 
    {
        if (this.source == null) this.source = this.GetComponent<AudioSource>();
	}
	void Update () 
    {
        if (!this.loop)
        {
            this.audioLengthLeft -= Time.deltaTime;
            if (this.audioLengthLeft <= 0.0f)
            {
                this.source.Stop();
                this.gameObject.SetActive(false);
            }
        }
        if(this.isFadeOut)
        {
            this.timeElapsedFadeOut += Time.deltaTime;
            this.source.volume = Mathf.Lerp(this.startVolume, 0.0f, this.timeElapsedFadeOut / this.timeLengthFadeOut);
            if(this.timeElapsedFadeOut >= this.timeLengthFadeOut)
            {
                StopSound();
            }
        }
	}
    public void PlaySound(AudioClip clip,bool loop = false,float volume = 1.0f)
    {
        this.gameObject.SetActive(true);
        this.isFadeOut = false;
        this.clip = clip;
        this.source.clip = this.clip;
        this.source.loop = loop;
        this.source.volume = volume;
        this.audioLength = this.clip.length;
        this.audioLengthLeft = this.audioLength;
        this.startVolume = volume;
        this.source.Play();
    }
    public void StopSound()
    {
        this.source.Stop();
        this.gameObject.SetActive(false);
    }
    public void Fadeout(float time = 0.5f)
    {
        this.timeLengthFadeOut = time;
        this.timeElapsedFadeOut = 0.0f;        
        this.isFadeOut = true;
    }
}
