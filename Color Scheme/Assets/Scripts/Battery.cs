using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractableObject {

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
