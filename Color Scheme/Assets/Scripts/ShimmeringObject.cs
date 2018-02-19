using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringObject : ComplexPaintableObject {

    float chargeLevel = 0;

    [Header("Configuration Values")]
    public float decayRate = .3f;
    public float meltingPoint = .4f;

    bool solid = false;

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

    protected override void DoUpdate() {
        base.DoUpdate();
        chargeLevel = Mathf.Max(chargeLevel - decayRate * Time.deltaTime, 0);
        if (solid && chargeLevel < meltingPoint) {
            DeSolidify();
        }
    }

    public override void Paint(Color c) {
        //TODO if the object is not active you cant paint it
        base.Paint(c);
    }

    public void Charge(float c) {
        chargeLevel = Mathf.Min(chargeLevel + c, 1f);
        if (!solid && chargeLevel > meltingPoint) {
            Solidify();
        }
    }

    private void Solidify() {
        Debug.Log(name + " is becoming solid");
        solid = true;
    }

    private void DeSolidify() {
        Debug.Log(name + " is becoming not solid");
        solid = false;
    }
}
