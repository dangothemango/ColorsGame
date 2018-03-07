using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChain : ButtonableObject {

    public float triggerTime;
    public float activeTime;
    public Battery[] batteries;
    public Color lightColor = Color.blue;

    bool active = false;

    int batteryIndex = 0;
    float t = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (active) {
            t += Time.deltaTime;
            if (t > triggerTime) {
                t = 0;
                StartCoroutine(LightandUnlight(batteries[batteryIndex++]));
                if (batteryIndex >= batteries.Length) {
                    active = false;
                }
            }
        }
	}

    public override void OnPressed(Color c) {
        base.OnPressed(c);
        if (!active) {
            active = true;
            t = triggerTime;
            batteryIndex = 0;
        }
    }

    IEnumerator LightandUnlight(Battery b) {
        b.Paint(lightColor);
        yield return new WaitForSeconds(activeTime);
        b.Paint(Color.black);
    }
}
