using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRoomResetter : MonoBehaviour 
{
	[SerializeField] RaceShade shade;
	[SerializeField] Battery light;

	Vector3 initialShadePosition;
	Color initialLightColor;

	// Use this for initialization
	void Start () 
	{
		initialShadePosition = shade.transform.position;
		initialLightColor = light.Color;
	}

	void OnTriggerEnter(Collider other)
	{
		shade.transform.position = initialShadePosition;
		light.Paint(initialLightColor);
	}
}
