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

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
        paint = GetComponent<PaintableObject>();
        originalPosition = transform.position;
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    // Update is called once per frame
    void Update () {
        DoUpdate();
	}

    public override void Interact() {
        if (interactable) {
            StartCoroutine(Depress());
        }
    }

    IEnumerator Depress() {
        interactable = false;
        Vector3 destination = originalPosition - transform.up * depressionDistance;
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, destination, t / depressionTime);
            yield return null;
        }
        transform.position = destination;
        OnPress();
        StartCoroutine(Lift());
    }

    void OnPress() {
        Debug.Log(string.Format("{0} Pressed", gameObject.name));
        foreach (ButtonableObject p in connectedObjects) {
            p.OnPressed(paint.color);
        }
        if (GameManager.INSTANCE.debug) {
            paint.Paint(Random.ColorHSV());
        }
    }

    IEnumerator Lift() {
        Vector3 start = transform.position;
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, originalPosition, t / depressionTime);
            yield return null;
        }
        transform.position = originalPosition;
        interactable = true;
    }

}
