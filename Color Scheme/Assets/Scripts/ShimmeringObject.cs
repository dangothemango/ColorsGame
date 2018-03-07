using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringObject : ComplexPaintableObject {

    [Header("Configuration Values")]
    public float decayRate = .3f;
    public float meltingPoint = .4f;
    public float chargeLevel = 0;
    Material m;

    [Header("Audio Inputs")]
    [SerializeField] protected AudioClip freeze;
    [SerializeField] protected AudioClip melt;

    bool charging = false;

    protected AudioSource sound;

    public float ChargeLevel {
        get {
            return chargeLevel;
        }
        set {
            chargeLevel = value;
            if (m != null) {
                m.SetFloat("_Controller", chargeLevel);
                //m.SetFloat("_RippleScale", (1 - chargeLevel) * .1f);
                //m.SetFloat("_RippleSpeed", (1 - chargeLevel));
            }
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
	}

    protected override void DoStart() {
        base.DoStart();
        Renderer r = GetComponent<Renderer>();
        m = r == null ? null : r.material;
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {
        base.DoUpdate();
        if (!charging) {
            ChargeLevel = Mathf.Max(ChargeLevel - decayRate * Time.deltaTime, 0);
        } else {
            charging = false;
        }
    }

    public override void Paint(Color c) {
        //TODO if the object is not active you cant paint it
        base.Paint(c);
    }

    public void Charge(float c) {
        charging = true;
        ChargeLevel = Mathf.Min(ChargeLevel + c, 1f);
    }

    protected virtual void Solidify() {
        solid = true;
        sound.clip = freeze;
        sound.Play();
        gameObject.layer = LayerMask.NameToLayer("SolidShimmering");
    
    }

    protected virtual void DeSolidify() {
        solid = false;
        sound.clip = melt;
        sound.Play();
        gameObject.layer = LayerMask.NameToLayer("LiquidShimmering");
    }
}
