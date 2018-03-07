using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDependentMovingPlatform : Platform_Movement_Script 
{
	// Update is called once per frame
	new protected void Update () 
	{
		if (attachedObject != null)
			base.Update();
	}
}
