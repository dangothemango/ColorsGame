using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonConfigurator : MonoBehaviour {

    public Transform[] playerSpawns;
    public int dungeonID = 0;

    public Narrator.NarrativeID narrativeID;

    string saveString;

	// Use this for initialization
	void Start () {
        saveString = "NarrativeComplete" + narrativeID.ToString();
        if (GameManager.INSTANCE.LoadSomething(saveString) == null) {
            GameManager.INSTANCE.narrator.TriggerSequentialNarrative(narrativeID);
        }
        GameManager.INSTANCE.SaveSomething(saveString, "true");
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(string.Format("Moving from {0} to {1}", GameManager.INSTANCE.currentDungeon == null ? 0 : GameManager.INSTANCE.currentDungeon.dungeonID, dungeonID));
        Player.INSTANCE.startLocation = playerSpawns[GameManager.INSTANCE.currentDungeon == null ? 0 : GameManager.INSTANCE.currentDungeon.dungeonID];
        Player.INSTANCE.resetPosition();
        if (GameManager.INSTANCE.currentDungeon != null) {
            Destroy(GameManager.INSTANCE.currentDungeon.gameObject);
        }
        GameManager.INSTANCE.currentDungeon = this;
	}
}
