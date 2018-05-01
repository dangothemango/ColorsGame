using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItem))]
public class PlayerItemPickup : InteractableObject 
{	

	// Use this for initialization
	void Start () 
	{
        if (GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(GetComponent<PlayerItem>().itemKey)) != null && !this.gameObject.name.Contains("Clone")) {
            Destroy(this.gameObject);
        }
        GetComponent<PlayerItem>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Interact()
	{
		Player player = Player.INSTANCE;
		GetComponent<PlayerItem>().enabled = true;
		player.addItem(GetComponent<PlayerItem> ());
        GetComponent<Collider>().isTrigger = true;
		Destroy(this);
	}
}
