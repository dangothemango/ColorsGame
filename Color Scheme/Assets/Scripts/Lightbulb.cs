﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

    [Header("Object References")]
    public Battery battery;
    new public ParticleSystem particleSystem;
    particleAttractorLinear particleAttractor;


    [Header("Configuration Values")]
    public float chargeRate = .5f;
    public Renderer externalRenderer;

	[SerializeField] bool useRaycast = false;
    [SerializeField]
    bool isFlorescent = false;

    Light lightSource;
    Renderer r;
    Material lightMat;


    // Use this for initialization
    void Awake() {
        r = externalRenderer != null ? externalRenderer : GetComponent<Renderer>();
        particleAttractor = GetComponentInChildren<particleAttractorLinear>();
        if (particleAttractor != null) { 
        particleAttractor.targets.Clear();
        particleAttractor.enabled = false;
        }
        lightSource = GetComponentInChildren<Light>();
        foreach (Material m in r.materials) {
            if (m.name.ToLower().StartsWith("lightbulbs")) {
                lightMat = m;
            }
        }
	}

    private void Start() {
        if (battery != null) {
            OnBatteryChange(battery.Color);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnBatteryChange(Color c) {
        lightMat.SetColor("_EmissionColor", c);
        lightSource.color = c;
        if (particleSystem != null) {
            if (c == Color.black) {
                particleSystem.Stop();
            }
            else {
                try {
                    var main = particleSystem.main;
                    main.startColor = c;
                    particleSystem.Play();
                }
                catch (System.Exception e) {
                    Debug.LogWarning(e.StackTrace);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        Vector3 oPos = other.transform.position;
		if (isFlorescent || lightSource.type == LightType.Point || (useRaycast && RayCastLight(other)) || ObjectInCone(oPos)) {
            ShimmeringObject s = other.GetComponent<ShimmeringObject>();
            Flashlight f = other.GetComponent<Flashlight>();
            if (s != null) {
                //Debug.Log("Charging");
                if (battery.Color == s.Color) {
                    s.Charge(chargeRate * Time.deltaTime);
                    if (particleAttractor != null && !particleAttractor.targets.Contains(s.transform)) {
                        particleAttractor.targets.Add(s.transform);
                        particleAttractor.enabled = true;
                    }
                }
            } else if (f != null) {
                f.Charge(chargeRate * Time.deltaTime);
                if (particleAttractor != null && !particleAttractor.targets.Contains(f.transform)) {
                    particleAttractor.targets.Add(f.transform);
                    particleAttractor.enabled = true;
                }
            }
        }
        else if (particleAttractor != null && particleAttractor.targets.Contains(other.transform)) {
            particleAttractor.targets.Remove(other.transform);
            if (particleAttractor.targets.Count == 0) {
                particleAttractor.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (particleAttractor != null && particleAttractor.targets.Contains(other.transform)) {
            particleAttractor.targets.Remove(other.transform);
            if (particleAttractor.targets.Count == 0) {
                particleAttractor.enabled = false;
            }
        }
    }

	private bool RayCastLight(Collider other)
	{
		return (Physics.Raycast(transform.position, transform.up, lightSource.range));
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
