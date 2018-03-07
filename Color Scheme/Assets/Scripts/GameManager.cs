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

    Dictionary<string,string> savedGameState;

	// Use this for initialization
	void Awake () {
		if (INSTANCE != null) {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
        savedGameState = new Dictionary<string, string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveSomething(string key, string data) { 
        savedGameState[key] = data;
    }

    public string LoadSomething(string key) {
        if (savedGameState.ContainsKey(key)) {
            return savedGameState[key];
        }
        return null;
    }  


}
