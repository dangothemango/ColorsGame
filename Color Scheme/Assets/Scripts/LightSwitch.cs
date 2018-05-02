using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : InteractableObject {

    public GameObject[] activationList;
    public Material skybox;
    public GameObject switchObject;

	// Use this for initialization
	void Start () {
        DoStart();
	}

    protected override void DoStart() {
        base.DoStart();

        foreach (GameObject go in activationList) {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        DoUpdate();
	}

    public override void Interact() { 
        foreach (GameObject go in activationList) {
            go.SetActive(true);
        }
        RenderSettings.skybox = skybox;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;

        Vector3 rot = switchObject.transform.localRotation.eulerAngles;
        rot.y += 180;
        switchObject.transform.localRotation = Quaternion.Euler(rot);

        interactable = false;
    }
}
