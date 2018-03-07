using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : Button {

    //Probably a better way than having two lists but fuck it
    public PaintableObject[] resetObjects;
    Color[] originalColors;

    // Use this for Awake
    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
    }

    // Use this for initialization
    void Start () {
        DoStart();
        originalColors = new Color[resetObjects.Length];
        for (int i = 0; i< resetObjects.Length; i++) {
            if (resetObjects[i] != null) {
                originalColors[i] = resetObjects[i].Color;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void OnPress() {
        for (int i = 0; i< resetObjects.Length; i++) {
            if (resetObjects[i] != null) {
                resetObjects[i].Paint(originalColors[i]);
            }
        }
    }
}
