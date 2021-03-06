﻿using System.Collections;
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
	}

	public override bool CanUseOn(InteractableObject target) {
        if (hasShade) {
            ShadeCage cage = target.GetComponent<ShadeCage>();
            return (cage != null && !cage.IsOccupied());
        }
        else
            return (target.GetComponent<Shade>() != null);
	}

	//do what eyedropper do!!
	public override void UseOn (InteractableObject target)
    {			
		if (hasShade && target.GetComponent<ShadeCage>())
		{
			target.GetComponent<ShadeCage>().ImprisonShade(shade.shadeColor);
            GetComponent<Renderer>().material.color = Color.white;
            hasShade = false;
			shade = null;
			return;
		}
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
        if (io.GetComponent<ShadeCage>() != null) {
            return secondaryTooltip;
        }
        return primaryTooltip;
	}

    private IEnumerator SampleShade(Shade shade) {

        Debug.Log(gazedColor);
        Color c = shade.GetComponent<Shade>().shadeColor;
        
        while (c.a >= 0.0f)
        {
            gazedColor = shade.shadeColor;
            Color temp = gazedColor;
            temp.a = 1.0f;
            GetComponent<Renderer>().material.color = Color.Lerp(Color.white, temp, alpha);
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
			hasShade = true;
        }
    }
}
