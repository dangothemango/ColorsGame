using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shade : SimplePaintableObject {

    protected bool dying;
    public Color killColor;
    private bool highlightShade;
    public Color shadeColor;
    public bool shadeIsInteractedWith = false;

    // Use this for initialization
    protected virtual void Start () {
        dying = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (dying)
        {
            // Do Dying Stuff
        }
	}

    protected IEnumerator replenishShade()
    {
        while (shadeColor.a < 1.0 && !shadeIsInteractedWith)
        {
            shadeColor.a += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }



    // 
    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Invoke("die", 2.0f);
        }
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    public void onGazeExit()
    {
        // stop shade emitting particles or glowing
        highlightShade = false;
    }
}
