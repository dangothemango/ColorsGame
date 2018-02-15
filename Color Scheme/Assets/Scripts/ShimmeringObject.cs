using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmeringObject : PaintableObject {

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

    protected override void Paint(Color c) {
        //TODO if the object is not active you cant paint it
        base.Paint(c);
    }
}
