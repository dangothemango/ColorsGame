using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatedDoor : ButtonableObject {

    public float openTime = 1f;
    public GameObject mesh;

    float startY;
    Coroutine doorMovement;
    Collider collider;

    private void Awake() {
        DoAwake();
    }

    private void Start() {
        DoStart();
    }

    protected override void DoStart() {
        base.DoStart();
        startY = mesh.transform.localPosition.y;
        collider = GetComponent<Collider>();
    }

    private void Update() {
        DoUpdate();
    }

    public void TriggerOpen() {
        if (doorMovement == null) {
            doorMovement = StartCoroutine(Open());
        }
        
    }

    void TriggerClose() {
        if (doorMovement == null) {
            StartCoroutine(Close());
        }
        
    }

    IEnumerator Open() {
        Debug.Log("Opening");
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, -startY, o.z);
        while (t < openTime) {
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        collider.enabled = false;
        doorMovement = null;
    }

    IEnumerator Close() {
        Debug.Log("Closing");
        collider.enabled = true;
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, startY, o.z);
        while (t < openTime) {
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.transform.localPosition = d;
        doorMovement = null;
    }

}

