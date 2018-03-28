using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour {
	public float decay;
	float creationTime;
	// Use this for initialization
	void Start () {
		creationTime = Time.time; 
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, decay);
	}

	void onCollision(Collision col){

	}
}
