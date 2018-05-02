using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ColorWheelRoom : MonoBehaviour {

    public GameObject roof;
    public FuseBox[] fuseBoxes;
    public Renderer r;
    public Collider c;

    public float time = 2f;
    public float liftSpeed = 3f;

    private void Start() {
        InvokeRepeating("CheckSolution", 1f, 1f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            TriggerEndgame();
        }
    }

    public void CheckSolution() {
        foreach (FuseBox f in fuseBoxes) {
            if (!f.hasFuse) {
                return;
            }
        }
        TriggerEndgame();
        CancelInvoke();
    }

    void TriggerEndgame() {
        //TODO add narrative elements
        r.enabled = c.enabled = true;
        StartCoroutine(ScaleRoofAndSelf());
    }

    IEnumerator ScaleRoofAndSelf() {
        float t = 0;
        Vector3 roofScale = roof.transform.localScale;
        Vector3 selfScale = transform.localScale;
        Vector3 start = new Vector3(selfScale.x, 0, selfScale.z);
        while (t < time) {
            roof.transform.localScale = Vector3.Lerp(roofScale, Vector3.zero, t / time);
            transform.localScale = Vector3.Lerp(start, selfScale, t / time);
            yield return null;
            t += Time.deltaTime;
        }
        roof.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        GetComponent<ReverseNormals>().InvertNormals();
        other.GetComponent<FirstPersonController>().m_StickToGroundForce = 0;
        other.GetComponent<FirstPersonController>().m_GravityMultiplier = 0;
    }

    private void OnTriggerStay(Collider other) {

        Debug.Log("F");
        other.transform.position += Vector3.up * liftSpeed * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<ReverseNormals>().InvertNormals();
        other.GetComponent<FirstPersonController>().m_StickToGroundForce = 10;
        other.GetComponent<FirstPersonController>().m_GravityMultiplier = 2;
    }

}
