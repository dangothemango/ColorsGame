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
    public KeyCode EYEDROPPER = KeyCode.Alpha3;
    public KeyCode PAUSE_GAME = KeyCode.Escape;
    public KeyCode SKIP_NARRATIVE = KeyCode.Space;

    [Header("Game System References")]
    public Narrator narrator;

    [Header("Audio")]
    public AudioSource mainAudioSource;
    public AudioClip puzzleCompleted;
    public AudioClip shadeSound;
    public AudioClip shadeDeath;
    public float shadeMinFreq;
    public float shadeMaxFreq;

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

    public DungeonConfigurator currentDungeon;

    Dictionary<string, string> currentState;
    
    
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
        currentState = StateLoader.LoadState();
    }
	
	// Update is called once per frame
	void Update () {
        if (debug && Input.GetKeyDown(KeyCode.Slash)) {
            OnPuzzleCompleted(PUZZLE_ID.DEBUG);
        }
	}

    public void SaveSomething(string key, string data) {
        if (currentState.ContainsKey(key)) {
            currentState[key] = data;
        }
        else {
            currentState.Add(key, data);
        }
    }



    public string LoadSomething(string key) {
        if (currentState.ContainsKey(key)) {
            return currentState[key];
        }
        return null;
    }

    public void OnPuzzleCompleted(PUZZLE_ID p = PUZZLE_ID.NONE) {
        mainAudioSource.PlayOneShot(puzzleCompleted);
    } 

    public string GetItemSaveString(KeyCode item) {
        return item.ToString() + "PlayerItem";
    }

    public static void LoadScene(int scene) {
        Initiate.Fade(scene, Random.ColorHSV(), 1f);
    }

    private void OnDestroy() {
        if (INSTANCE == this) {
            StateLoader.SaveState(currentState);
        }
    }


}
