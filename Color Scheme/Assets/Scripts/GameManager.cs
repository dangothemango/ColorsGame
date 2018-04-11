using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Controls")]
    public KeyCode INTERACT = KeyCode.E;
	public KeyCode ITEM_SECONDARY = KeyCode.Mouse1;
	public KeyCode NO_ITEM = KeyCode.BackQuote;
	public KeyCode BUCKET = KeyCode.Alpha1;
	public KeyCode FLASHLIGHT = KeyCode.Alpha2;
	public KeyCode PAUSE_GAME = KeyCode.Escape;

    [Header("Game System References")]
    public Narrator narrator;

    [Header("Audio")]
    public AudioSource mainAudioSource;
    public AudioClip puzzleCompleted;

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

    public int currentDungeon = 0;

    Dictionary<string,string> savedGameState;
    
    public enum PUZZLE_ID {
        NONE,
        DEBUG,
        PUZZLE_LISTENER,
        BUTTON_CODE,
        COLOR_WHEEL
    }

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
        if (debug && Input.GetKeyDown(KeyCode.Slash)) {
            OnPuzzleCompleted(PUZZLE_ID.DEBUG);
        }
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

    public void OnPuzzleCompleted(PUZZLE_ID p = PUZZLE_ID.NONE) {
        mainAudioSource.PlayOneShot(puzzleCompleted);
    } 

}
