using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

    [Header("Object References")]
    public Battery battery;

    Light lightSource;
    Renderer r;
    Material lightMat;

	// Use this for initialization
	void Start () {
        r = GetComponent<Renderer>();
        lightSource = GetComponentInChildren<Light>();
        foreach (Material m in r.materials) {
            Debug.Log(m.name);
            if (m.name.ToLower().StartsWith("lightbulbs")) {
                Debug.Log(m.name);
                lightMat = m;
            }
        }
        if (battery != null) {
            OnBatteryChange(battery.color);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBatteryChange(Color c) {
        lightMat.SetColor("_EmissionColor", c);
        lightSource.color = c;
    }
}
