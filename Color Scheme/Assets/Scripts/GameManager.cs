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

    [Header("Debug")]
    public bool debug = false;

    public static GameManager INSTANCE;

    public DungeonConfigurator currentDungeon;

    Dictionary<string, string> currentState;
    HashSet<string> saveNames;
    string currentSaveName;
    string saveNamesString = "SAVE_NAMES";

    [System.Serializable]
    public class SaveData {

        public SaveItem[] saveItems;

        public SaveData(int count) {
            saveItems = new SaveItem[count];
        }

        public static SaveData FromDictionary(Dictionary<string,string> dict) {
            SaveData sd= new SaveData(dict.Count);

            int i = 0;
            foreach (KeyValuePair<string,string> pair in dict) {
                sd.saveItems[i] = new SaveItem(pair);
                i++;
            }

            return sd;
        }

        public static Dictionary<string,string> ToDictionary(SaveData s) {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (SaveItem si in s.saveItems) {
                dict.Add(si.key, si.value);
            }

            return dict;
        }

    }

    [System.Serializable]
    public class SaveItem {
        public string key;
        public string value;

        public SaveItem(KeyValuePair<string,string> pair) {
            this.key = pair.Key;
            this.value = pair.Value;
        }
    }

    [System.Serializable]
    public class SaveNames {
        public string[] names;

        public static SaveNames FromSet(HashSet<string> hs) {
            SaveNames sn = new SaveNames();
            sn.names = new string[hs.Count];

            int i = 0;
            foreach (string s in hs) {
                sn.names[i] = s;
                i++;
            }

            return sn;
        } 

        public static HashSet<string> ToSet(SaveNames sn) {
            return new HashSet<string>(sn.names);
        }
    }
    
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
        currentState = new Dictionary<string, string>();
        if (PlayerPrefs.HasKey(saveNamesString)) {
            saveNames = SaveNames.ToSet(JsonUtility.FromJson<SaveNames>(PlayerPrefs.GetString(saveNamesString)));
        } else {
            saveNames = new HashSet<string>();
        }
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

    public void SaveState(Dictionary<string, string> state, string saveName) {
        if (saveName == null) {
            //TODO Prompt user for savename
            saveName = System.DateTime.Now.ToString(new System.Globalization.CultureInfo("en-US"));
        }
        Debug.Log("Saving with name: " + saveName);
        string saveData = JsonUtility.ToJson(SaveData.FromDictionary(state),true);
        if (saveNames.Add(saveName)) {
            PlayerPrefs.SetString(saveNamesString, JsonUtility.ToJson(SaveNames.FromSet(saveNames)));
        }

        PlayerPrefs.SetString(saveName, saveData);
    }

    public Dictionary<string,string> LoadState(string saveName) {
        if (!saveNames.Contains(saveName)) return new Dictionary<string, string>();
        SaveData sd = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(saveName));
        currentSaveName = saveName;

        return SaveData.ToDictionary(sd);
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
            SaveState(currentState, currentSaveName);
        }
    }


}
