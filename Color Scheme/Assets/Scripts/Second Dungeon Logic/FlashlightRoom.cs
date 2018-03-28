using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRoom : ButtonableObject 
{
	[SerializeField] ButtonActivatedDoor buttonDoor;
	[SerializeField] LightActivatedDoor lightDoor;
	[SerializeField] Lightbulb lightooooooooooooooo;

	// Use this for initialization
	void Start() 
	{
		buttonDoor.gameObject.SetActive(false);
		lightDoor.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		lightooooooooooooooo.enabled = false;
		buttonDoor.gameObject.SetActive(true);
	}

	public override void OnPressed(Color c)
	{
		lightooooooooooooooo.enabled = true;
		buttonDoor.gameObject.SetActive(false);
		Destroy(buttonDoor.gameObject);
		Destroy(gameObject);
	}
}
