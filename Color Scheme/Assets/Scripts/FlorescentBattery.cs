using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorescentBattery : Battery {

    [Header("Object References")]
    public Lightbulb[] extraLightbulbs;

    private void Awake() {
        DoAwake();
    }

    // Use this for initialization
    void Start() {
        DoStart();
    }

    // Update is called once per frame
    void Update() {
        DoUpdate();
    }

    public override void Paint(Color c) {
        base.Paint(c);
        foreach (Lightbulb lb in extraLightbulbs) { 
        lb.OnBatteryChange(c);
    }
    }
}
