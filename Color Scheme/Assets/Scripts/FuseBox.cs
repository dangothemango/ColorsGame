using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : InteractableObject 
{
	public Color col;
	[SerializeField] LevelDoor door;

    private void Awake() 
	{
        DoAwake();
    }

    // Use this for initialization
    void Start () 
	{
        DoStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
        DoUpdate();
	}

	protected override void DoStart()
	{
		GetComponent<Renderer>().material.color = col;
		door.gameObject.SetActive(false);
	}

    public override void Interact() 
	{
		if (Player.INSTANCE.carriedFuse.col == col)
		{
			Player.INSTANCE.carriedFuse.transform.SetParent(transform);
			Player.INSTANCE.carriedFuse.transform.localPosition = Vector3.zero;
			Player.INSTANCE.carriedFuse = null;
			door.gameObject.SetActive(true);
		}
    }
}
