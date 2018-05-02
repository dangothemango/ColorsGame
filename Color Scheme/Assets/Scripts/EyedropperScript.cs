using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just implementing interaction with shades

public class EyedropperScript : PlayerItem {
	[SerializeField] private SimplePaintableObject paint;
	[SerializeField] private Shade shade;
	// [SerializeField] private AudioSource sampleAudio;
	// [SerializeField] private AudioSource releaseAudio;

	Color gazedColor;
    float alpha = 0.0f;
	bool hasPaint = false;
	bool hasShade = false;

	void Awake() {
		// sampleAudio = GetComponent<AudioSource> ();
		// releaseAudio = GetComponent<AudioSource> ();
		if (!shade) {
			//shade = GetComponentInChildren
		}
	}

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gazedColor);
        Color temp = gazedColor;
        temp.a = 1.0f;
        GetComponent<Renderer>().material.color =  Color.Lerp(Color.white, temp, alpha);
	}

	public override bool CanUseOn(InteractableObject target) {
		return target.GetComponent<Shade>() != null;
	}

	//do what eyedropper do!!
	public override void UseOn (InteractableObject target)
    {			
		shade = target.GetComponent<Shade> ();
        shade.shadeIsInteractedWith = true;

        StartCoroutine(SampleShade(shade));
		// through interactable script attached to Shade grab Shade script
		// while interacting
			// freeze shade
			// lower alpha value of its colour
			// emit particles from transform position
			// direct them to particle attractor at front of eyedropper tool
			// if battery
		// call its deposit function
	}

	public override void Filter (Color c) {}

	public override void SecondaryUsage(){


	}

	public override Sprite GetTooltipIcon(InteractableObject io, out Color c) {
		c = gazedColor;
		return primaryTooltip;
	}

    private IEnumerator SampleShade(Shade shade) {
        Color c = shade.GetComponent<Shade>().shadeColor;
        while (c.a >= 0.0f)
        {
            gazedColor = shade.shadeColor;
            alpha = 1.0f - c.a;
            if (Input.GetKeyUp(GameManager.INSTANCE.INTERACT)) {
                shade.shadeIsInteractedWith = false;
                break;
            }
            c.a -= 0.01f;
            //Debug.Log(shade.GetComponent<Shade>().shadeColor);
            shade.GetComponent<Shade>().shadeColor = c;
            yield return new WaitForSeconds(0.01f);
        }
        if (c.a <= 0.0f)
        {
            Destroy(shade.gameObject, 0.0f);
        }
    }
}
