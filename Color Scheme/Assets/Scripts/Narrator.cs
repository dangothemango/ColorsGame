using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour {

    public enum NarrativeID {
        OnGameStart,
        OnEnterFirstDungeon,
        OnDeath,
        OnRespawn,
        OnDebugTrigger
    }

    [System.Serializable]
    public class Narrative {
        public string[] gameStart;
        public string[] enteringFirstDungeon;
        public string[] deathMessages;
        public string[] respawnMessages;
        public string[] debugMessages;
    }

    public TextAsset narrativeText;

    Narrative narrative;
    bool narrationActive = true;

	// Use this for initialization
	void Start () {
		if (narrativeText == null) {
            Debug.LogError("No Narrative Present");
            narrationActive = false;
        }
        narrative = JsonUtility.FromJson<Narrative>(narrativeText.text);
	}
	
	public void TriggerNarrative(NarrativeID id) {
        if (!narrationActive) return;
        switch (id) {
            case NarrativeID.OnGameStart:
                ShowMessage(narrative.gameStart);
                break;
            case NarrativeID.OnEnterFirstDungeon:
                ShowMessage(narrative.enteringFirstDungeon);
                break;
            case NarrativeID.OnDeath:
                ShowMessage(narrative.deathMessages);
                break;
            case NarrativeID.OnRespawn:
                ShowMessage(narrative.respawnMessages);
                break;
            case NarrativeID.OnDebugTrigger:
                ShowMessage(narrative.debugMessages);
                break;
            default:
                break;
        }
    }

    void ShowMessage(string[] phrases) {
        if (phrases.Length == 0) return;
        int index = Random.Range(0, phrases.Length);
        string message = phrases[index];
        if (message == null) return;
        phrases[index] = null;
        Debug.Log(message);
    }
}
