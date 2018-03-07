using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableObject : ButtonableObject {

    public Color color;
	public bool colorChanges = false;
    
    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        Paint(color);
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected override void DoStart() {
		
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {}

    public virtual void Paint(Color c) {
        color = c;
    }

    public override void OnPressed(Color c) {
        base.OnPressed(c);
        this.Paint(c);
    }
}
