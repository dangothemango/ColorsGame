using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedLaserProjectile : InteractableObject {
	GameObject projectileSpawnPoint;
	public GameObject projectile;
	public float speed;
	public bool continous = false;
	private float shotTime = 0f;

	// Use this for initialization
	void Start () {
		projectileSpawnPoint = this.gameObject.transform.Find ("ProjectileSpawnPoint").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (continous && Time.time - 1.0f > shotTime)
		{
			Shoot();
			shotTime = Time.time;
		}
	}

	public override void Interact() {
		if (!continous)
			Shoot();
	}

	private void Shoot()
	{
		GameObject clone;
		clone = Instantiate (projectile,
			projectileSpawnPoint.transform.position,
			projectileSpawnPoint.transform.rotation);
		clone.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.transform.TransformDirection (Vector3.forward * speed);
	}
}
