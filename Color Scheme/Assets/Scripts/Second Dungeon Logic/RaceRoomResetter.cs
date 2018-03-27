using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRoomResetter : MonoBehaviour 
{
	[SerializeField] RaceShade shade;
	[SerializeField] Battery battery;
	[SerializeField] Platform_Movement_Script platform;

	Vector3 initialShadePosition;
	Color initialLightColor;

	// Use this for initialization
	void Start() 
	{
		initialShadePosition = shade.transform.position;
		initialLightColor = battery.Color;
		shade.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		shade.transform.position = initialShadePosition;
		shade.enabled = true;
		battery.Paint(initialLightColor);
		platform.transform.position = platform.Waypoints[0].position;
	}
}
