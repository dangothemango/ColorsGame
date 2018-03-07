using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatformMover : ButtonableObject 
{

	// Use this for initialization
	void Start () 
	{
		GetComponent<Platform_Movement_Script>().enabled = false;
	}

	public override void OnPressed(Color c)
	{
		GetComponent<Platform_Movement_Script>().enabled = true;
	}
}
