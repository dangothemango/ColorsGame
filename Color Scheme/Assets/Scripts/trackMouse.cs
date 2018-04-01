using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackMouse : MonoBehaviour {

	public float distance = 10.0f;

	void Start(){
		Cursor.visible = false;
	}

	void Update(){
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 pos = r.GetPoint (distance);
		transform.position = pos;
	}
}
