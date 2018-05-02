using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCageButton : ButtonableObject 
{
	[SerializeField] int numCages = 3;
	[SerializeField] ButtonableObject door;
	Renderer rend;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		rend.enabled = false;
	}

	public override void OnPressed(Color c)
	{
		numCages--;
		if (numCages == 0)
		{
			rend.enabled = true;
			door.OnPressed(c);
			this.enabled = false;
		}
	}
}
