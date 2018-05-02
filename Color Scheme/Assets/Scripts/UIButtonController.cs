using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour {

	public void LoadSceneByIndex(int sceneIndex){
        GameManager.LoadScene(sceneIndex);
	}

    public void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }

	public void ExitToDesktop(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
