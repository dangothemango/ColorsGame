using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableObject : ButtonableObject {

    public Color color;

    Renderer r;

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {}

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected override void DoStart() {
        r = GetComponent<Renderer>();
        r.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {}

    public virtual void Paint(Color c) {
        color = c;
        r.material.color = color;
    }

    public override void OnPressed(Color c) {
        base.OnPressed(c);
        this.Paint(c);
    }
}
