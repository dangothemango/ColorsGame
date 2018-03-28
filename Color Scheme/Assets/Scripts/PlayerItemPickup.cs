using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItem))]
public class PlayerItemPickup : InteractableObject 
{	

	// Use this for initialization
	void Start () 
	{
		GetComponent<PlayerItem>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Interact()
	{
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		GetComponent<PlayerItem>().enabled = true;
		player.addItem(GetComponent<PlayerItem>());
        GetComponent<Collider>().isTrigger = true;
		Destroy(this);
	}
}
