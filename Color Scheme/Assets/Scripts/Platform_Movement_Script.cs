using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movement_Script : MonoBehaviour {

	public enum Direction
	{
		Zward,
		Xward,
		Yward
	}
	public float speed = 1.0f;
	public Direction dir = Direction.Zward;
	Transform trans;

	// Use this for initialization
	void Start () 
	{
		bool isShimmering = true;
		trans = GetComponent<Transform>(); 
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		switch (dir) 
		{
		case Direction.Xward:
			{
				trans.Translate (speed, 0.0f, 0.0f);
				break;
			}
		case Direction.Yward:
			{
				trans.Translate (0.0f, speed, 0.0f);
				break;
			}
		case Direction.Zward:
			{
				trans.Translate (0.0f, 0.0f, speed);
				break;
			}
		}
	}
}
