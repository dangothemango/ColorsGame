using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenRightColor : ButtonableObject 
{
	[SerializeField] ButtonActivatedDoor[] doors;
	[SerializeField] Color correctColor;

	public override void OnPressed(Color c)
	{
		if (c == correctColor)
		{
			foreach (ButtonActivatedDoor door in doors)
				door.TriggerOpen();
		}
	}
}
