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
		//while clicking
			//sampleTarget
	}

	public override bool CanUseOn(InteractableObject target) {
		return target.GetComponent<Shade>() != null;
	}

	//do what eyedropper do!!
	public override void UseOn (InteractableObject target) {			
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
        while (c.a >= 0.0f) {
            if (Input.GetKeyUp(GameManager.INSTANCE.INTERACT)) {
                shade.shadeIsInteractedWith = false;
                yield return null;
            }
            c.a -= 0.001f;
            Debug.Log(c.a);
            yield return new WaitForSeconds(0.001f);
        }
        currentColor = shade.shadeColor;
        currentColor.a = 1.0f;
        Destroy(shade.gameObject, 0.0f);
        yield return null;
    }
}
