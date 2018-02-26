using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableInteraction : InteractableObject {

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

    public override void onGazeEnter(PlayerItem currentItem) {
        if (currentItem is Bucket) {
            base.onGazeEnter(currentItem);
        }
    }

    public override void onGazeExit() {
        base.onGazeExit();
    }
}
