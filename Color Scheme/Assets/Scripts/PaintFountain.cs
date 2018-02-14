using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFountain : InteractableObject {

	// Use this for initialization
	void Start () {
        DoStart();
	}
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    public override void Interact() {
        base.Interact();
    }
}
