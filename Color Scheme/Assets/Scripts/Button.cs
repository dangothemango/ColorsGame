using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableObject {

    [Header("Button Helpers")]
    public float depressionTime = .5f;
    public float depressionDistance = .5f;
    public ButtonableObject[] connectedObjects;

    //Component references
    PaintableObject paint;

    //World references
    Vector3 originalPosition;

    Vector3 OriginalPosition {
        get {
            return originalPosition;
        }
        set {
            originalPosition = value;
        }
    }

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
        paint = GetComponent<PaintableObject>();
        OriginalPosition = transform.position;
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    // Update is called once per frame
    void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {
        base.DoUpdate();
    }

    public override void Interact() {
        if (interactable) {
            StartCoroutine(Depress());
        }
    }

    IEnumerator Depress() {
        interactable = false;
        Vector3 destination = OriginalPosition - transform.up * depressionDistance;
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(OriginalPosition, destination, t / depressionTime);
            yield return null;
        }
        transform.position = destination;
        OnPress();
        StartCoroutine(Lift());
    }

    protected virtual void OnPress() {
        foreach (ButtonableObject p in connectedObjects) {
            p.OnPressed(paint.Color);
        }
    }

    IEnumerator Lift() {
        Vector3 start = transform.position;
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, OriginalPosition, t / depressionTime);
            yield return null;
        }
        transform.position = OriginalPosition;
        interactable = true;
    }

}
