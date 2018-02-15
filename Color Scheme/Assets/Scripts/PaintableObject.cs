using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableObject : MonoBehaviour {

    public Color color;

    Renderer r;

    private void Awake() {
        DoAwake();
    }

    protected virtual void DoAwake() {}

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected virtual void DoStart() {
        r = GetComponent<Renderer>();
        r.material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected virtual void DoUpdate() {}

    public virtual void Paint(Color c) {
        color = c;
        r.material.color = color;
    }
}
