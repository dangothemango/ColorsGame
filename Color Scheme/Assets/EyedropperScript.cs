using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just implementing interaction with shades

public class EyedropperScript : PlayerItem {
	[SerializeField] private SimplePaintableObject paint;
	[SerializeField] private Shade shade;
	private AudioSource sampleAudio;
	private AudioSource releaseAudio;

	public Color currentColor = Color.clear;
	bool hasPaint = false;
	bool hasShade = false;

	void Awake() {
		sampleAudio = GetComponent<AudioSource> ();
		releaseAudio = GetComponent<AudioSource> ();
		if (!shade) {
			shade = GetComponentInChildren
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
		return target == shade ? true : false;
	}

	//do what eyedropper do!!
	public override void UseOn (InteractableObject target) {			
		
	}

	public override void Filter (Color c){

	}

	public override void SecondaryUsage(){


	}

	public override Sprite GetTooltipIcon(InteractableObject io, out Color c) {

		c = currentColor;
		return primaryTooltip;
	}

}
