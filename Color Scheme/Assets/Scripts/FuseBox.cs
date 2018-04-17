using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : InteractableObject 
{
	public Color col;
	[SerializeField] LevelDoor door;
    Fuse fuse;

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
        base.DoStart();
		GetComponent<Renderer>().material.color = col;
        fuse = GetComponentInChildren<Fuse>();
        fuse.gameObject.SetActive(false);
        if (GameManager.INSTANCE.LoadSomething(col.ToString() + "FuseBox") != null) {
            EnableFuse();
        }
        else {
            door.gameObject.SetActive(false);
        }
	}

    public override void Interact() 
	{
		if (Player.INSTANCE.carriedFuse.col == col)
		{
            EnableFuse();
			Destroy(Player.INSTANCE.carriedFuse.gameObject);
			Player.INSTANCE.carriedFuse = null;
            GameManager.INSTANCE.SaveSomething(col.ToString() + "FuseBox", "true");
		}
    }

    void EnableFuse() {
        fuse.gameObject.SetActive(true);
        fuse.setColor();
        Destroy(fuse);
        door.gameObject.SetActive(true);
    }
}
