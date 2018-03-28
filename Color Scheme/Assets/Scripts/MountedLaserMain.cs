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

		//restore variables for each child of the laser gun object
		//some can be ignored due to implementation changes i.e...
		tPosRestore = t.localPosition;
		tRotRestore = t.localRotation;
		crystalPosRestore = crystal.transform.localPosition; //ignore
		crystalRotRestore = crystal.transform.localRotation; //ignore
		laserRingPosRestore = laserRing.transform.localPosition; //ignore
		laserRingRotRestore = laserRing.transform.localRotation; //ignore
		laserRingSmallPosRestore = laserRingSmall.transform.localPosition; //ignore
		laserRingSmallRotRestore = laserRingSmall.transform.localRotation; //ignore

		cam = Player.INSTANCE.GetComponent<Camera> ();

		equiped = false;
		isReset = true;
		aim = Aim.REST;
		// currentAim = t.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (transform.rotation == tRotRestore) {
			//main body floats only when in its neutral 
			t.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate) * amplitude, 0.0f);
		}
		//float children parts of the laser (outer rings and inner crystal)
		crystal.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * 1.5f *floatRate) * amplitude * 0.9f, 0.0f);
		laserRing.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate * 0.25f) * amplitude * 0.2f, 0.0f);
		laserRingSmall.transform.Translate (0.0f, Mathf.Sin (Time.fixedTime * Mathf.PI * floatRate * 0.5f) * amplitude * 0.2f, 0.0f);
	}

	public override void Interact() {
		if (interactable) {
			StartCoroutine(Reaim());
		}
	}


	/// <summary>
	/// Reaim this instance.
	/// I am trying to create a class that changes the orientation of the gun each time the player interacts with it.
	/// It would have its rotation lerped with each interaction locking further interaction till the movement was done.
	/// </summary>
	IEnumerator Reaim() {
		interactable = false;
		Quaternion newAim = tRotRestore;
		Quaternion currentAim = transform.rotation;
		if (aim == Aim.WEST){
			aim = Aim.REST;
		}
		else{
			aim++;
		}
		float elapsedTime = 0.0f;
		Debug.Log (aim);

		switch (aim) { //enumerate between all the possible directions toggle by player interactions
		case Aim.NORTH:
			 {
				newAim = Quaternion.Euler (90.0f, 0.0f, 0.0f);
				break;
			}
		case Aim.SOUTH:
			{
				newAim = Quaternion.Euler (-90.0f, 0.0f, 0.0f);
				break;
			}
		case Aim.EAST:
			{
				newAim = Quaternion.Euler (0.0f, 0.0f, 90.0f);
				break;
			}
		case Aim.WEST:
			{
				newAim = Quaternion.Euler (0.0f, 0.0f, -90.0f);
				break;
			}
		}
		Debug.Log (newAim);

		// lerp the movement of the laser main body
		while (elapsedTime < aimDuration){
			elapsedTime += Time.deltaTime;
			transform.localRotation = Quaternion.Lerp(currentAim, newAim, elapsedTime / aimDuration);
			yield return null;
		}
		// currentAim = newAim;
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