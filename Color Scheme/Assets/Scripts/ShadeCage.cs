using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeCage : InteractableObject 
{
	private bool isOccupied = false;
	[SerializeField] ButtonableObject[] affectedObjects;
	[SerializeField] Renderer[] shade;

	// Use this for initialization
	void Start() 
	{
		isOccupied = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsOccupied()
	{
		return isOccupied;
	}

	public void ImprisonShade(Color c)
	{
		foreach (Renderer rend in shade)
		{
			rend.enabled = true;
			rend.material.SetColor("_EmissionColor", c);
		}
		foreach (ButtonableObject obj in affectedObjects)
		{
			obj.OnPressed(c);
		}
		isOccupied = true;
	}
}
