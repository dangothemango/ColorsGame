using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivatedDoor : ShimmeringObject {

    public float openTime = 1f;
    public GameObject mesh;

    float startY;

    private void Awake() {
        DoAwake();
    }

    private void Start() {
        DoStart();
    }

    protected override void DoStart() {
        base.DoStart();
        startY = mesh.transform.localPosition.y;
    }

    private void Update() {
        DoUpdate();
        if (Input.GetKeyDown(KeyCode.O)) {
            Solidify();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            DeSolidify();
        }
    }

    protected override void Solidify() {
        solid = true;
        StopAllCoroutines();
        StartCoroutine(Open());
        gameObject.layer = LayerMask.NameToLayer("LiquidShimmering");
    }

    protected override void DeSolidify() {
        solid = false;
        StopAllCoroutines();
        StartCoroutine(Close());
        gameObject.layer = LayerMask.NameToLayer("SolidShimmering");
    }

    IEnumerator Open() {
        Debug.Log("Opening");
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, -startY, o.z);
        while (t<openTime){
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.SetActive(false);
    }

    IEnumerator Close() {
        Debug.Log("Closing");
        mesh.SetActive(true);
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, startY, o.z);
        while (t < openTime) {
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.transform.localPosition = d;
    }

}
