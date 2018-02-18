using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonableObject : MonoBehaviour {

    private void Awake() {
        DoAwake();
    }

    protected virtual void DoAwake() { }

    // Use this for initialization
    void Start() {
        DoStart();
    }

    protected virtual void DoStart() {}

    // Update is called once per frame
    void Update() {
        DoUpdate();
    }

    protected virtual void DoUpdate() { }

    public virtual void OnPressed(Color c) { }
}
