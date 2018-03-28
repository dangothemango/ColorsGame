using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : PlayerItem {

	static Color[] colorList = { Color.red, Color.green, Color.blue, new Color(1, 1, 0, 1), Color.magenta, Color.cyan, Color.white };

    [Header("Object References")]
    public Battery battery;
    public Renderer colorableSurface;

    int colorIndex = 0;
    Color currentColor;
    AudioSource sound;
    bool on = false;

    bool IsOn {
        get {
            return on;
        }
        set {
            on = value;
            if (value) {
                battery.Paint(currentColor);
            } else {
                battery.Paint(Color.black);
            }
        }
    }


    Color CurrentColor {
        get {
            return currentColor;
        } set {
            currentColor = value;
            colorableSurface.material.color = value;
            if (IsOn) {
                battery.Paint(value);
            }
        }
    }
    public override bool CanUseOn(InteractableObject target) {
        return true;
    }

    public override void Filter(Color c) {}

    public override void SecondaryUsage() {
        CurrentColor = colorList[(++colorIndex)%colorList.Length];
    }

    public override void UseOn(InteractableObject target) {
        IsOn = !IsOn;
        sound.Play();
    }

    private void Awake() {
        CurrentColor = colorList[colorIndex];
    }

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Sprite GetTooltipIcon(InteractableObject io, out Color c) {
		c = Color.white;
		return primaryTooltip;
    }
}
