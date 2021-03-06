﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shade : SimplePaintableObject {

    protected bool dying;
    private Color killColor;
    private bool highlightShade;
    public Color shadeColor;
    public bool shadeIsInteractedWith = false;
    bool replenishing = false;

	[SerializeField] Renderer[] rends;
	[SerializeField] float colorFactor = 0.5f;

    // [Header("Audio Frequency Controls")]
    float minTime;
    float maxTime;
    float audioTimer;

    float baseAlphaForColor;

    AudioSource mouth;

    AudioClip sound;
    AudioClip deathSound;

    // Use this for initialization
    protected virtual void Start () {
		killColor = shadeColor;
        dying = false;
        minTime = GameManager.INSTANCE.shadeMinFreq;
        maxTime = GameManager.INSTANCE.shadeMaxFreq;
        setAudioTimer();
        sound = GameManager.INSTANCE.shadeSound;
        deathSound = GameManager.INSTANCE.shadeDeath;
        mouth = GetComponent<AudioSource>();
        mouth.clip = sound;
        baseAlphaForColor = shadeColor.a;
        GetComponent<Transform>().Find("Shade_Improved/Body (Cloth)").GetComponent<Renderer>().material.SetColor("Albedo", shadeColor);
        GetComponent<Transform>().Find("Shade_Improved/Head (Static)").GetComponent<Renderer>().material.SetColor("Albedo", shadeColor);
    
		foreach (Renderer r in rends)
		{
			r.material.EnableKeyword("_EMISSION");
			r.material.SetColor("_EmissionColor", shadeColor / (1 / colorFactor));
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (dying)
        {
            // Do Dying Stuff
            return;
        }

        // Not Dying stuff
        audioTimer -= Time.deltaTime;
        if(audioTimer <= 0f)
        {
            mouth.Play();
            setAudioTimer();
        }

        //Debug.Log(shadeColor.a);
        if (!shadeIsInteractedWith && shadeColor.a < baseAlphaForColor)
        {
            shadeColor.a += 1.0f / 60.0f;
            //Debug.Log(shadeColor);
            //StartCoroutine(replenishShade());
        }
        GetComponent<Transform>().Find("Shade_Improved/Body (Cloth)").GetComponent<Renderer>().material.color = shadeColor;
        GetComponent<Transform>().Find("Shade_Improved/Head (Static)").GetComponent<Renderer>().material.color = shadeColor;

    }

    protected IEnumerator replenishShade()
    {
        replenishing = true;
        while (shadeColor.a < baseAlphaForColor)
        {
            if (shadeIsInteractedWith) break;
            Debug.Log("replenishing!!");
            shadeColor.a += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        replenishing = false;
    }

    private void setAudioTimer()
    {
        float rand = Random.value;
        audioTimer = (minTime * (1.0f - rand) + maxTime * rand);
    }

    // 
    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Invoke("die", 2.0f);
        }
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    public void onGazeExit()
    {
        // stop shade emitting particles or glowing
        highlightShade = false;
    }
}
