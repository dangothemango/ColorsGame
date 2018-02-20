using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePaintableObject : PaintableObject {

	Renderer r;

    private void Awake() {
        DoAwake();
    }

    // Use this for initialization
    void Start() {
        DoStart();
    }

    protected override void DoAwake() {
        r = GetComponent<Renderer>();
		base.DoAwake();
    }

    // Update is called once per frame
    void Update() {
        DoUpdate();
    }

    public override void Paint(Color c) {
        base.Paint(c);
        r.material.color = c;
    }
}
