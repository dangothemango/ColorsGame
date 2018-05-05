using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItem))]
public class PlayerItemPickup : InteractableObject 
{	

	// Use this for initialization
	void Start () 
	{
        DoStart();
        if (GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(GetComponent<PlayerItem>().GetType().Name)) != null && !this.gameObject.name.Contains("Clone")) {
            Debug.Log("ObjectFound");
            Destroy(this.gameObject);
        }
        GetComponent<PlayerItem>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () 
	{
        DoUpdate();
	}

	public override void Interact()
	{
        this.onGazeExit();
		Player player = Player.INSTANCE;
        Debug.Log("Bucket!");
        GetComponent<PlayerItem>().enabled = true;
		player.addItem(GetComponent<PlayerItem> ());
        GetComponent<Collider>().isTrigger = true;
		Destroy(this);
	}
}
