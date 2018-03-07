using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Controls")]
    public KeyCode INTERACT = KeyCode.E;
	public KeyCode ITEM_SECONDARY = KeyCode.Mouse1;
	public KeyCode NO_ITEM = KeyCode.BackQuote;
	public KeyCode BUCKET = KeyCode.Alpha1;

    [Header("Game System References")]
    public Narrator narrator;

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

    public int currentDungeon = 0;

	// Use this for initialization
	void Awake () {
		if (INSTANCE != null) {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
