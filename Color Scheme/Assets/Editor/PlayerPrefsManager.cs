using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerPrefsManager : MonoBehaviour {

	
    [MenuItem("Player Prefs/Clear Player Prefs")]
    public static void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
