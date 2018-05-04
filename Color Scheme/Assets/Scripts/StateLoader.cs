using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateLoader :MonoBehaviour {

    [SerializeField]
    GameObject buttonPrefab;
    [SerializeField]
    GameObject contentWindow;

    static string saveNamesString = "SAVE_NAMES";
    static HashSet<string> names;
    public static HashSet<string> saveNames {
        get {
            if (names == null) {
                if (PlayerPrefs.HasKey(saveNamesString)) {
                    names = SaveNames.ToSet(JsonUtility.FromJson<SaveNames>(PlayerPrefs.GetString(saveNamesString)));
                }
                else {
                    names = new HashSet<string>();
                }
            }
            return names;
        }
    }
    static string currentSaveName;
    public static string CurrentSaveName {
        get {
            if (currentSaveName == null) {
                    currentSaveName = System.DateTime.Now.ToString(new System.Globalization.CultureInfo("en-US"));
            }
            return currentSaveName;
        }
    }

    [System.Serializable]
    public class SaveData {

        public SaveItem[] saveItems;

        public SaveData(int count) {
            saveItems = new SaveItem[count];
        }

        public static SaveData FromDictionary(Dictionary<string, string> dict) {
            SaveData sd = new SaveData(dict.Count);

            int i = 0;
            foreach (KeyValuePair<string, string> pair in dict) {
                sd.saveItems[i] = new SaveItem(pair);
                i++;
            }

            return sd;
        }

        public static Dictionary<string, string> ToDictionary(SaveData s) {
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

        public SaveItem(KeyValuePair<string, string> pair) {
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

    public static void SaveState(Dictionary<string, string> state) {

        Debug.Log("Saving with name: " + CurrentSaveName);
        string saveData = JsonUtility.ToJson(SaveData.FromDictionary(state), true);
        if (saveNames.Add(CurrentSaveName)) {
            PlayerPrefs.SetString(saveNamesString, JsonUtility.ToJson(SaveNames.FromSet(saveNames)));
        }

        PlayerPrefs.SetString(CurrentSaveName, saveData);
    }

    public static Dictionary<string, string> LoadState() {
        if (!saveNames.Contains(CurrentSaveName)) return new Dictionary<string, string>();
        SaveData sd = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(CurrentSaveName));

        return SaveData.ToDictionary(sd);
    }

    public static void SetCurrentSaveName(string name) {
        currentSaveName = name;
    }

    private void OnEnable() {
        RectTransform contentRect = contentWindow.transform as RectTransform;
        foreach (RectTransform t in contentRect) {
            Destroy(t.gameObject);
        }
        int topPadding = (int)((buttonPrefab.transform as RectTransform).rect.height * .2f);
        int heightStep = topPadding * 5;
        int count = 0;
        foreach (string name in saveNames) {
            GameObject newButton = Instantiate(buttonPrefab, contentRect);
            RectTransform newT = newButton.transform as RectTransform;

            Vector3 pos = newT.anchoredPosition3D;
            pos.y =  -topPadding - (count * heightStep);
            newT.anchoredPosition3D = pos;
            
            Text text = newButton.GetComponentInChildren<Text>();
            text.text = name;

            UnityEngine.UI.Button b = newButton.GetComponent<UnityEngine.UI.Button>();
            b.onClick.AddListener(delegate { SetCurrentSaveName(name); });
            b.onClick.AddListener(delegate { GameManager.LoadScene(1); });

            count += 1;
        }
    }

}
