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
        startY = mesh.transform.localScale.y;
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
    }

    protected override void DeSolidify() {
        solid = false;
        StopAllCoroutines();
        StartCoroutine(Close());
    }

    IEnumerator Open() {
        float t = 0;
        Vector3 o = mesh.transform.localScale;
        Vector3 d = new Vector3(o.x, 0, o.z);
        while (t<openTime){
            mesh.transform.localScale = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.SetActive(false);
    }

    IEnumerator Close() {
        mesh.SetActive(true);
        float t = 0;
        Vector3 o = mesh.transform.localScale;
        Vector3 d = new Vector3(o.x, startY, o.z);
        while (t < openTime) {
            mesh.transform.localScale = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.transform.localScale = d;
    }

}
