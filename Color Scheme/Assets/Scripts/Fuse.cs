using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : InteractableObject 
{
	public Color col;

	// Use this for initialization
	void Start () 
	{
        setColor();
        if (GetComponentInParent<FuseBox>() ==null && GameManager.INSTANCE.LoadSomething(col.ToString() + "FuseBox") != null) {
            Destroy(this.gameObject);
        }
	}

	public override void Interact()
	{
		Player.INSTANCE.carriedFuse = this;
        Player.INSTANCE.hasFuse = true;
		transform.SetParent(Player.INSTANCE.transform);
		GetComponent<Renderer>().enabled = false;
	}

    public void setColor() {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", col);
    }
}
