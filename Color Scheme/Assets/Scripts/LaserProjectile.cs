using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour {
	public float decay;
	float creationTime;
	public float reflectOnChargeLevel = 0.3f;
	public LayerMask layerMask;
	public enum colorPad {
		RED,
		GREEN,
		BLUE,
		YELLOW,
		CYAN,
		WHITE,
		BLACK,
		MAGENTA
	}
	public colorPad setColor = colorPad.BLACK;

	private float miscObjectCollisionActiveTime = 3.0f;

	// Use this for initialization
	void Start () {
		creationTime = Time.time;
		switch (setColor) {
		case colorPad.BLACK:
			{
				this.GetComponent<Renderer> ().material.color = Color.black;
				break;
			}
		case colorPad.RED:
			{
				this.GetComponent<Renderer> ().material.color = Color.red;
				break;
			}
		case colorPad.GREEN:
			{
				this.GetComponent<Renderer> ().material.color = Color.green;
				break;
			}
		case colorPad.BLUE:
			{
				this.GetComponent<Renderer> ().material.color = Color.blue;
				break;
			}
		case colorPad.YELLOW:
			{
				this.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 0.0f, 0.0f);
				break;
			}
		case colorPad.CYAN:
			{
				this.GetComponent<Renderer> ().material.color = Color.cyan;
				break;
			}
		case colorPad.WHITE:
			{
				this.GetComponent<Renderer> ().material.color = Color.white;
				break;
			}
		case colorPad.MAGENTA:
			{
				this.GetComponent<Renderer> ().material.color = Color.magenta;
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		//Destroy (this.gameObject, decay);
	}

	/// <summary>
	/// Raises the collision enter event.
	/// Kills player and fades screen to color of this projectile or
	/// is destroyed on collision with Solidshimmering layer shimmering objects or
	/// kills shades of a color similar to this projectile. It destroys itself either way or
	/// is destroy on collision with all other objects
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "Player") {
			//send color of laser projectile to user screen overlay
			other.gameObject.SendMessage ("hitByLaserTrigger", this.GetComponent<Renderer> ().material.color); 
			//trigger player respawn
			other.gameObject.SendMessage ("die", false);
			Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Shimmering") {
			Destroy (this.gameObject, 0.0f);
			//TODO: fire particle effects on destruction
		} else if (other.gameObject.GetComponent<ReflectiveSurface> () != null) {
			this.GetComponent<Renderer> ().material.color = other.gameObject.GetComponent<Renderer> ().material.color; 
		} else if (other.gameObject.GetComponent<Shade> () != null) {
			if (other.gameObject.GetComponent<Renderer> ().material.color == this.GetComponent<Renderer> ().material.color) {
				Destroy (other.gameObject, 0.0f);
			}
			Destroy (this.gameObject, 0.0f);
		} else if (creationTime + miscObjectCollisionActiveTime < Time.time) {
			Destroy (this.gameObject, 0.0f);
		}
	}
}
