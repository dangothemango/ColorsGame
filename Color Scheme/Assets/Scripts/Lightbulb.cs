using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

    [Header("Object References")]
    public Battery battery;

    [Header("Configuration Values")]
    public float chargeRate = .5f;

    Light lightSource;
    Renderer r;
    Material lightMat;

	// Use this for initialization
	void Awake () {
        r = GetComponent<Renderer>();
        lightSource = GetComponentInChildren<Light>();
        foreach (Material m in r.materials) {
            Debug.Log(m.name);
            if (m.name.ToLower().StartsWith("lightbulbs")) {
                Debug.Log(m.name);
                lightMat = m;
            }
        }
	}

    private void Start() {
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

    private void OnTriggerStay(Collider other) {
        Vector3 oPos = other.transform.position;
        if (lightSource.type == LightType.Point || ObjectInCone(oPos)) {
            
            ShimmeringObject s = other.GetComponent<ShimmeringObject>();
            if (s == null) return;
            Debug.Log("Charging");
            if (battery.color == s.color) {
                s.Charge(chargeRate * Time.deltaTime);
            }
        }
    }

    private bool ObjectInCone(Vector3 oPos) {
        if (lightSource.type != LightType.Spot) {
            return false;
        }
        //TODO there has to be a better way to do this mathematically.
        Vector3 direction = (lightSource.transform.position - transform.position).normalized;
        Vector3 objectDirection = oPos-lightSource.transform.position;
        float coneHeight = lightSource.range;
        float halfAngle = lightSource.spotAngle/2;
        
        float yDist = Vector3.Dot(objectDirection, direction);
        if (yDist <= 0 || yDist > coneHeight) return false;

        float xDist = Mathf.Sqrt(Mathf.Pow(objectDirection.magnitude, 2) - Mathf.Pow(yDist, 2));

        float coneRadius = Mathf.Tan(halfAngle)*yDist;

        if (xDist < coneRadius) return true;
        return false;
    }
}
