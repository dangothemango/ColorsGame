using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : InteractableObject 
{
	[SerializeField] Color col;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer>().material.SetColor("_EmissionColor", col);
	}

	public override void Interact()
	{
		Player.INSTANCE.carriedFuse = this;
		transform.SetParent(Player.INSTANCE.transform);
		GetComponent<Renderer>().enabled = false;
	}
}
