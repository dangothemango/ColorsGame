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

    [Header("Game System References")]
    public Narrator narrator;

    [Header("Audio")]
    public AudioSource mainAudioSource;
    public AudioClip puzzleCompleted;

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

    public DungeonConfigurator currentDungeon;
    
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
#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll();
#endif
    }
	
	// Update is called once per frame
	void Update () {
        if (debug && Input.GetKeyDown(KeyCode.Slash)) {
            OnPuzzleCompleted(PUZZLE_ID.DEBUG);
        }
	}

    public void SaveSomething(string key, string data) {
        PlayerPrefs.SetString(key, data);
    }

    public string LoadSomething(string key) {
        if (PlayerPrefs.HasKey(key)) {
            return PlayerPrefs.GetString(key);
        }
        return null;
    }

    public void OnPuzzleCompleted(PUZZLE_ID p = PUZZLE_ID.NONE) {
        mainAudioSource.PlayOneShot(puzzleCompleted);
    } 

    public string GetItemSaveString(KeyCode item) {
        return item.ToString() + "PlayerItem";
    }

}
