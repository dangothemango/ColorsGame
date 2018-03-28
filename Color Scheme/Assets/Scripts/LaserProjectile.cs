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

	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "Player") {
			//send color of laser projectile to user screen overlay
			other.gameObject.SendMessage("hitByLaserTrigger", this.GetComponent<Renderer> ().material.color); 
			//trigger player respawn
			other.gameObject.SendMessage("die", false);
		}
	}
}
