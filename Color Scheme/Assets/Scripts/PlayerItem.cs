﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerItem : MonoBehaviour 
{
	public KeyCode itemKey;
	public Vector3 itemOffset = Vector3.zero;
    public Vector3 itemRotation = Vector3.zero;
    public float itemScale = 1f;
    public Sprite primaryTooltip;
    public Sprite secondaryTooltip;

	// Use this for initialization
	void Start() 
	{

	}
	
	// Update is called once per frame
	void Update() 
	{
		
	}

	// Can this item be used on the target object?
	public abstract bool CanUseOn(InteractableObject target);

	public abstract void UseOn(InteractableObject target);

	public abstract void Filter(Color c);

	public abstract void SecondaryUsage();

    public abstract Sprite GetTooltipIcon(InteractableObject io, out Color c);
}
