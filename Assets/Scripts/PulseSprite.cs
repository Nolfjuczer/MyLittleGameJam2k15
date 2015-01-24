using UnityEngine;
using System.Collections;

public class PulseSprite : MonoBehaviour 
{
    private float timeMultiplier = 3.0f;

    public SpriteRenderer ren = null;
    private float timeElapsed = 0.0f;

    float minAlpha = 0.1f;
    float maxAlpha = 0.7f;

	void Start () 
    {
        if (this.ren == null) this.ren = this.GetComponent<SpriteRenderer>();
	}
	void Update () 
    {
        this.timeElapsed += Time.deltaTime * this.timeMultiplier;
	    if(ren != null)
        {
            Color tmpColor = ren.color;
            tmpColor.a = this.minAlpha + (this.maxAlpha - this.minAlpha) * ((1 + Mathf.Sin(this.timeElapsed)) / 2);
            ren.color = tmpColor;
        }
	}
}
