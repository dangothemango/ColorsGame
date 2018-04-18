using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonConfigurator : MonoBehaviour {

    public Transform[] playerSpawns;
    public int dungeonID = 0;

    public Narrator.NarrativeID narrativeID;

    public GameObject narrationDelayedObject;

    string saveString;
    float narrativeDelay = 1f;

	// Use this for initialization
	void Start () {
        saveString = "NarrativeComplete" + narrativeID.ToString();
        if (GameManager.INSTANCE.LoadSomething(saveString) == null) {
            StartCoroutine(WaitAndStartNarrative());
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

    IEnumerator WaitAndStartNarrative() {
        yield return new WaitForSeconds(narrativeDelay);
        GameManager.INSTANCE.narrator.narratorCallback += ActivateDelayedObject;
        GameManager.INSTANCE.narrator.TriggerNarrative(narrativeID, sequential: true);
    }

    void ActivateDelayedObject() {
        if (narrationDelayedObject == null) return;
        narrationDelayedObject.SetActive(true);
    }

}
