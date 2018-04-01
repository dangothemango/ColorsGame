using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour {

	public void LoadSceneByIndex(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}

	public void ExitToDesktop(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void ChangeMenu(Canvas options) {
		options.enabled = true;
		enabled = false;
		Debug.Log (11);
	}
}
