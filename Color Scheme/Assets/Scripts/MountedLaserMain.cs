using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedLaserMain : InteractableObject {
	Camera cam;
	Rigidbody rig;
	Transform t;
	GameObject crystal;
	GameObject laserRing;
	GameObject laserRingSmall;

	Vector3 tPosRestore;
	Quaternion tRotRestore;
	Vector3 crystalPosRestore;
	Quaternion crystalRotRestore;
	Vector3 laserRingPosRestore;
	Quaternion laserRingRotRestore;
	Vector3 laserRingSmallPosRestore;
	Quaternion laserRingSmallRotRestore;
	[HideInInspector] public bool equiped;
	bool isReset;
	public float floatRate = 0.1f;
	public float amplitude = 0.1f;
	Aim aim;
	Quaternion currentAim;
	public float aimTime = .5f;
	public float aimDuration = 1.0f;


	enum Aim{ // all local rotations i.e. w.r.t parent orientation
		REST,
		NORTH, // x-axis rotation positive
		SOUTH, // x-axis rotation negative
		EAST, // z-axis rotation negative
		WEST  // z-axis rotation positive
	}

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> (); 
		t = GetComponent<Transform> ();
		crystal = this.gameObject.transform.GetChild(0).gameObject;
		laserRing = this.gameObject.transform.GetChild(1).gameObject;
		laserRingSmall = this.gameObject.transform.GetChild(2).gameObject;

		tPosRestore = t.localPosition;
		tRotRestore = t.localRotation;
		crystalPosRestore = crystal.transform.localPosition;
		crystalRotRestore = crystal.transform.localRotation;
		laserRingPosRestore = laserRing.transform.localPosition;
		laserRingRotRestore = laserRing.transform.localRotation;
		laserRingSmallPosRestore = laserRingSmall.transform.localPosition;
		laserRingSmallRotRestore = laserRingSmall.transform.localRotation;

		cam = Player.INSTANCE.GetComponent<Camera> ();

		equiped = false;
		isReset = true;
		aim = Aim.REST;
		currentAim = t.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (transform.rotation == tRotRestore) {
			t.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate) * amplitude, 0.0f);
		}
		crystal.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * 1.5f *floatRate) * amplitude * 0.9f, 0.0f);
		laserRing.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate * 0.25f) * amplitude * 0.2f, 0.0f);
		laserRingSmall.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate * 0.5f) * amplitude * 0.2f, 0.0f);
	}

	void Interact() {
		if (interactable) {
			StartCoroutine(Reaim());
		}
	}

	IEnumerator Reaim() {
		interactable = false;
		Quaternion newAimAngles = tRotRestore;
		aim++;
		float elapsedTime = 0.0f;

		switch (aim) {
		case Aim.NORTH:
			{
				newAimAngles = currentAim;
				newAimAngles.x += 90.0f;
				break;
			}
		case Aim.SOUTH:
			{
				newAimAngles = currentAim;
				newAimAngles.x += -90.0f;
				break;
			}
		case Aim.EAST:
			{
				newAimAngles = currentAim;
				newAimAngles.z += -90.0f;
				break;
			}
		case Aim.WEST:
			{
				newAimAngles = currentAim;
				newAimAngles.z += 90.0f;
				break;
			}
		}

		while (elapsedTime < aimDuration){
			elapsedTime += Time.deltaTime;
			transform.rotation = Quaternion.Lerp(currentAim, newAimAngles, elapsedTime / aimDuration);
			yield return null;
		}
		currentAim = newAimAngles;
		interactable = true;
	}
}


//firing objects
/*public class ExampleClass : MonoBehaviour {
	public Rigidbody projectile;
	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Rigidbody clone;
			clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * 10);
		}
	}
}*/