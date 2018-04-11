using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void ExitToMainMenu (){

	}

	public void Exit(){

	}
}
