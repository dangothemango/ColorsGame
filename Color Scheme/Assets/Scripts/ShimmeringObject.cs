using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringObject : ComplexPaintableObject {

    [Header("Configuration Values")]
    public float decayRate = .3f;
    public float meltingPoint = .4f;
	Material m;

    float chargeLevel = 0;

    public float ChargeLevel {
        get {
            return chargeLevel;
        }
        set {
            chargeLevel = value;
			m.SetFloat ("Controller", ChargeLevel);
            if (solid && chargeLevel < meltingPoint) {
                DeSolidify();
            }
            else if (!solid && chargeLevel > meltingPoint) {
                Solidify();
            }
        }
    }

    protected bool solid = false;

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
    }

    // Use this for initialization
    void Start () {
        DoStart();
		m = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {
        base.DoUpdate();
        //ChargeLevel = Mathf.Max(ChargeLevel - decayRate * Time.deltaTime, 0);
    }

    public override void Paint(Color c) {
        //TODO if the object is not active you cant paint it
        base.Paint(c);
    }

    public void Charge(float c) {
        ChargeLevel = Mathf.Min(ChargeLevel + c, 1f);
    }

    protected virtual void Solidify() {
        Debug.Log(name + " is becoming solid");
		solid = true;
		gameObject.layer = SortingLayer.GetLayerValueFromName("SolidShimmering");
    
    }

    protected virtual void DeSolidify() {
        Debug.Log(name + " is becoming not solid");
		solid = false;
        gameObject.layer = SortingLayer.GetLayerValueFromName("LiquidShimmering");
    }
}
