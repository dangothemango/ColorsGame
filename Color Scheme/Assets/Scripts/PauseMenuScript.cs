using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {
	

	void Start(){
	}

	void OnEnable(){
	}

	// Update is called once per frame
	void Update () {
	}

	public void Resume() {
		Player.INSTANCE.PauseGame ();
	}

	public void RestartFromCheckPoint(){
		Player.INSTANCE.resetPosition ();
	}

	public void Restart(){
		Player.INSTANCE.startLocation = GameManager.INSTANCE.currentDungeon.playerSpawns [0];
		Player.INSTANCE.resetPosition();
	}

	public void ExitToMainMenu (int menuIndex){
        GameManager.LoadScene(menuIndex);
	}

	public void Exit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

    public string saveName;
    public UnityEngine.UI.Button saveButton;
    public Color normalOverwrite;
    public Color highlightedOverwrite;
    public Color normalNew;
    public Color highlightedNew;

    public void checkOverwrite(string sn) {
        if (sn.Length > 0) {
            saveName = sn;
        }
        ColorBlock cb = saveButton.colors;

        if (StateLoader.saveNames.Contains(saveName)) {
            cb.normalColor = normalOverwrite;
            cb.highlightedColor = highlightedOverwrite;
            saveButton.GetComponentInChildren<Text>().text = "Overwrite";
        }
        else {
            cb.normalColor = normalNew;
            cb.highlightedColor = highlightedNew;
            saveButton.GetComponentInChildren<Text>().text = "SaveNew";
        }
 
        saveButton.colors = cb;
    }

    public void Save() {
        GameManager.INSTANCE.SaveNewState(saveName);
    }
}
