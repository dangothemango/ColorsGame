using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just implementing interaction with shades

public class EyedropperScript : PlayerItem {
	[SerializeField] private SimplePaintableObject paint;
	[SerializeField] private Shade shade;
	// [SerializeField] private AudioSource sampleAudio;
	// [SerializeField] private AudioSource releaseAudio;

	public Color currentColor = Color.clear;
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
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer> ().material.color = currentColor;
		//while clicking
			//sampleTarget
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
		c = currentColor;
		return primaryTooltip;
	}

    private IEnumerator SampleShade(Shade shade) {
        Color c = shade.GetComponent<Renderer>().material.color;
        while (c.a >= 0.0f)
        {
            currentColor = shade.shadeColor;
            currentColor.a = 1.0f - c.a;
            if (Input.GetKeyUp(GameManager.INSTANCE.INTERACT)) {
                shade.shadeIsInteractedWith = false;
                break;
            }
            c.a -= 0.01f;
            Debug.Log(shade.GetComponent<Renderer>().material.color);
            shade.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        if (c.a <= 0.0f)
        {
            Destroy(shade.gameObject, 0.0f);
        }
    }
}
