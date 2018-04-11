using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHelper : PlayerAttacher 
{
	[SerializeField] bool forward = true;

	protected override void Attach(Transform other)
	{
		(movingScript as PlayerDependentMovingPlatform).forward = forward;
		base.Attach(other);
	}
}
