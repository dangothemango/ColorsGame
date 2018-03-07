using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : ComplexPaintableObject {

    [Header("Object References")]
    public Lightbulb lightbulb;

    private void Awake() {
        DoAwake();
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    public override void Paint(Color c) {
        base.Paint(c);
        lightbulb.OnBatteryChange(c);
    }

}
