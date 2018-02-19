using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Base_Movement : MonoBehaviour 
{

	public float frequency = 1.0f;
	public float amplitude = 0.1f;
	Transform trans;

	// Use this for initialization
	void Start () 
	{
		trans = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//TODO: base rotation and settling
		trans.Translate(0.0f, Mathf.Sin(Time.fixedTime*Mathf.PI*frequency)*amplitude, 0.0f);
	}
}
