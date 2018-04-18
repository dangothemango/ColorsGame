using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("External References")]
    public TextAsset narrativeText;
    public GameObject subtitleGameObject;
    public Text subtitleText;

    [Header("Configuration Values")]
    public float showTimePerCharacter = .2f;

    static Narrative narrative;
    bool narrationActive = true;
    public delegate void NarratorCallback();
    public NarratorCallback narratorCallback;

	// Use this for initialization
	void Awake () {
		if (narrativeText == null) {
            Debug.LogError("No Narrative Present");
            narrationActive = false;
        }
        narrative = JsonUtility.FromJson<Narrative>(narrativeText.text);
	}
	
	public void TriggerNarrative(NarrativeID id, bool sequential = false, int index = 0) {
        if (!narrationActive) return;
        string[] activeNarrative = GetNarrativeById(id);
        StartCoroutine(ShowMessage(activeNarrative, sequential, index));
    }

    public string[] GetNarrativeById(NarrativeID id) {
        switch (id) {
            case NarrativeID.OnGameStart:
                return narrative.gameStart;
            case NarrativeID.OnEnterFirstDungeon:
                return narrative.enteringFirstDungeon;
            case NarrativeID.OnDeath:
                return narrative.deathMessages;
            case NarrativeID.OnRespawn:
                return narrative.respawnMessages;
            case NarrativeID.OnDebugTrigger:
                return narrative.debugMessages;
            default:
                return null;
        }
    }

    IEnumerator ShowMessage(string[] phrases,bool sequential, int i) {
        if (phrases.Length == 0) yield break;
        int index = i;
        if (sequential) {
            while (index < phrases.Length && phrases[index] == null) {
                index++;
            }
        }
        else {
            index = Random.Range(0, phrases.Length);
            if (phrases[index] == null) yield break;
        }
        string message = phrases[index];
        phrases[index] = null;
        Debug.Log("Displaying subtitle: " + message);
        yield return StartCoroutine(ShowAndHideSubtitle(message,index));
    }

    IEnumerator ShowAndHideSubtitle(string message,int index) {
        subtitleGameObject.SetActive(true);
        subtitleText.text = message;
        float t = 0;
        float duration = Mathf.Min(Mathf.Max(1f,showTimePerCharacter * message.Length),4f);
        while (t < duration) {
            t += Time.deltaTime;
            yield return null;
        }
        subtitleGameObject.SetActive(false);
        if (narratorCallback != null) {
            narratorCallback.Invoke();
        }
    }

    public void TriggerSequentialNarrative(NarrativeID id) {
        StartCoroutine(SequentialNarrative(id));
    }

    IEnumerator SequentialNarrative(NarrativeID id) {
        string[] sequence = GetNarrativeById(id);
        for (int i =0; i <sequence.Length; i++) {
            yield return ShowMessage(sequence, true, i);
        }
    }
}
