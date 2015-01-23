using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour 
{
    public Image image = null;
    public float progres = 0.0f;
    public int type = -1;
    public RectTransform rectTransform = null;

	void Start () 
    {
        if (this.image == null) this.image = this.GetComponent<Image>();
        if (this.rectTransform == null) this.rectTransform = this.GetComponent<RectTransform>();
	}
	void Update () 
    {
	
	}
}
