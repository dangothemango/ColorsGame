using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFountain : InteractableObject {

	[Header("Paint Fountain Parameters")]
	public Color col;

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

	// TODO: Do some animation of paint fountain filling paint bucket
    public override void Interact() {
        base.Interact();
    }
}
