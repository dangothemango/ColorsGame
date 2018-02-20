using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Controls")]
    public KeyCode INTERACT = KeyCode.E;
	public KeyCode NO_ITEM = KeyCode.BackQuote;
	public KeyCode BUCKET = KeyCode.Alpha1;

    [Header("Game System References")]
    public Narrator narrator;

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

	// Use this for initialization
	void Start () {
		if (INSTANCE != null) {
            this.enabled = false;
            return;
        }
        INSTANCE = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
