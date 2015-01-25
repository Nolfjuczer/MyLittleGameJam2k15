using UnityEngine;
using System.Collections;

public class Speaker : MonoBehaviour 
{
    public AudioSource source = null;
    public AudioClip clip = null;

    private float audioLength = 0.0f;
    private float audioLengthLeft = 0.0f;
    private bool loop = false;

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
	}
    public void PlaySound(AudioClip clip,bool loop = false,float volume = 1.0f)
    {
        this.gameObject.SetActive(true);
        this.clip = clip;
        this.source.clip = this.clip;
        this.source.loop = loop;
        this.source.volume = volume;
        this.audioLength = this.clip.length;
        this.audioLengthLeft = this.audioLength;
        this.source.Play();
    }
}
