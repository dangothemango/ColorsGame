using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivatedDoor : ShimmeringObject {

    public float openTime = 1f;
    public GameObject mesh;

    float startY;
    Coroutine doorMovement;

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
    }

    protected override void Solidify() {
        solid = true;
        if (doorMovement == null) {
            doorMovement = StartCoroutine(Open());
        }
        gameObject.layer = LayerMask.NameToLayer("LiquidShimmering");
        sound.clip = melt;
        sound.Play();
    }

    protected override void DeSolidify() {
        solid = false;
        if (doorMovement == null) {
            StartCoroutine(Close());
        }
        gameObject.layer = LayerMask.NameToLayer("SolidShimmering");
        sound.clip = freeze;
        sound.Play();
    }

    IEnumerator Open() {
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, -startY, o.z);
        while (t<openTime){
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.SetActive(false);
        doorMovement = null;
    }

    IEnumerator Close() {
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
        doorMovement = null;
    }

}
