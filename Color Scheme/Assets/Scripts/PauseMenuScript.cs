using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameManager.INSTANCE.LoadScene(menuIndex);
	}

	public void Exit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
