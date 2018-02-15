using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableObject {

    [Header("Button Helpers")]
    public float depressionTime = .5f;
    public float depressionDistance = .5f;

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
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
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position -= transform.up * Time.deltaTime / depressionTime * depressionDistance;
            yield return null;
        }
        OnPress();
        StartCoroutine(Lift());
    }

    void OnPress() {
        Debug.Log(string.Format("{0} Pressed", gameObject.name));
    }

    IEnumerator Lift() {
        float t = 0;
        while (t < depressionTime) {
            t += Time.deltaTime;
            transform.position += transform.up * Time.deltaTime / depressionTime * depressionDistance;
            yield return null;
        }
        interactable = true;
    }

}
