using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [Header("Panels")]
    public GameObject main;
    public GameObject loadSave;
    public GameObject options;

    void SetActivePanel(string panel) {
        main.SetActive(panel == "main");
        loadSave.SetActive(panel == "loadSave");
        options.SetActive(panel == "options");
    }

	// Use this for initialization
	void Start () {
        OpenMainPanel();
	}
	
    public void OpenLoadPanel() {
        SetActivePanel("loadSave");
    }

    public void OpenMainPanel() {
        SetActivePanel("main");
    }

    public void OpenOptionsPanel() {
        SetActivePanel("options");
    }
}
