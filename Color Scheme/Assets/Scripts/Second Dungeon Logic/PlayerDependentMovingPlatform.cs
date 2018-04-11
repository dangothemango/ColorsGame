using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDependentMovingPlatform : Platform_Movement_Script 
{
	[HideInInspector] public bool forward = true;

	// Update is called once per frame
	new protected void Update () 
	{
		if (attachedObject != null)
		{
			waypointIndex = forward ? 1 : 0;
			if (transform.position != Waypoints[waypointIndex].position)
				base.Update();
		}
	}
}
