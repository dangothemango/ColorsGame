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
        OnEnteringMainRoomD1
    }

    [System.Serializable]
    public class Narrative {
        public string[] gameStart;
        public string[] enteringFirstDungeon;
        public string[] deathMessages;
        public string[] respawnMessages;
        public string[] enteringMainRoomD1;
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
	
	public void TriggerNarrative(NarrativeID id, bool sequential = false) {
        return;
        if (!narrationActive) return;
        if (sequential) {
            TriggerSequentialNarrative(id);
            return;
        }
        StopAllCoroutines();
        string[] activeNarrative = GetNarrativeById(id);
        StartCoroutine(ShowMessage(activeNarrative, sequential, 0));
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
            case NarrativeID.OnEnteringMainRoomD1:
                return narrative.enteringMainRoomD1;
            default:
                return null;
        }
    }

    IEnumerator ShowMessage(string[] phrases,bool sequential, int i) {
        yield break;
        if (phrases.Length == 0) yield break;
        int index = i;
        if (sequential) {
            while (index < phrases.Length && phrases[index] == null) {
                index++;
            }
        }
        else {
            index = Random.Range(0, phrases.Length);
            StartCoroutine(ShowAndHideSubtitle(null));
            if (phrases[index] == null) yield break;
        }
        string message = phrases[index];
        phrases[index] = null;
        yield return StartCoroutine(ShowAndHideSubtitle(message));
    }

    IEnumerator ShowAndHideSubtitle(string message) {
        yield break;
        subtitleGameObject.SetActive(true);
        if (message != null) {
            subtitleText.text = message;
        }
        float duration = Mathf.Min(Mathf.Max(1f,showTimePerCharacter * subtitleText.text.Length),4f);
        yield return StartCoroutine(WaitForSecondsOrSkip(duration));
        subtitleGameObject.SetActive(false);

    }

    IEnumerator WaitForSecondsOrSkip(float d) {
        yield break;
        float t = 0;
        while (t < d && !Input.GetKeyDown(GameManager.INSTANCE.SKIP_NARRATIVE)) {
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    void TriggerSequentialNarrative(NarrativeID id) {
        return;
        if (!narrationActive) return;
        StopAllCoroutines();
        StartCoroutine(SequentialNarrative(id));
    }

    IEnumerator SequentialNarrative(NarrativeID id) {
        yield break;
        string[] sequence = GetNarrativeById(id);
        for (int i =0; i <sequence.Length; i++) {
            yield return ShowMessage(sequence, true, i);
        }
        if (narratorCallback != null) {
            narratorCallback.Invoke();
            narratorCallback = null;
        }
    }
}
