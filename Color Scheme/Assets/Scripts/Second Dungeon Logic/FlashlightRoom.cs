using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRoom : MonoBehaviour 
{
	[SerializeField] ButtonActivatedDoor entrance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;
		entrance.GetComponent<LightActivatedDoor>().enabled = false;
		entrance.enabled = true;
		entrance.TriggerClose();
		Destroy(gameObject);
	}
}
