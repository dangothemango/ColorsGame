using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexPaintableObject : PaintableObject {

    public Renderer[] paintedMeshes;
    public string[] variableColors;

    private void Awake() {
        DoAwake();
    }

    // Use this for initialization
    void Start() {
        DoStart();
    }

    // Update is called once per frame
    void Update() {
        DoUpdate();
    }

    public override void Paint(Color c) {
        base.Paint(c);
        foreach (Renderer ren in paintedMeshes) {
            foreach (string s in variableColors) {
                ren.material.SetColor(s, Color);
            }
        }
    }
}
