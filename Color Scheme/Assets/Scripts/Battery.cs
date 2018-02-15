using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : PaintableObject {

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
    
}
