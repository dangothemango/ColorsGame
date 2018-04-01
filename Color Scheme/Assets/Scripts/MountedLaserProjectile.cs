using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedLaserProjectile : InteractableObject {
	GameObject projectileSpawnPoint;
	public GameObject projectile;
	public float speed;

	// Use this for initialization
	void Start () {
		projectileSpawnPoint = this.gameObject.transform.Find ("ProjectileSpawnPoint").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Interact() {
		GameObject clone;
		clone = Instantiate (projectile,
			projectileSpawnPoint.transform.position,
			projectileSpawnPoint.transform.rotation);
		clone.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.transform.TransformDirection (Vector3.forward * speed);
	}
}
