using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : InteractableObject 
{
	public Color col;
	[SerializeField] LevelDoor door;
    [SerializeField]
    Battery battery;
    Fuse fuse;
    public bool hasFuse = false;

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
        fuse.col = col;
        fuse.gameObject.SetActive(false);
        if (GameManager.INSTANCE.LoadSomething(col.ToString() + "FuseBox") != null) {
            EnableFuse();
        }
        else if (door!=null){
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
        if (door != null) {
            door.gameObject.SetActive(true);
        }
        battery.Paint(col);
        hasFuse = true;
    }
}
