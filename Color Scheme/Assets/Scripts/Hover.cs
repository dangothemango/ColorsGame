using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {
	public float floatRate;
	public float amplitude = 0.5f;
	public bool On;
	Transform trans;
	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (On) {
			trans.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate) * amplitude, 0.0f);
			//print (Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate));
		}
	}
}
