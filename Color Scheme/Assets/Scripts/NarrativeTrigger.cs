﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour {

    public Narrator.NarrativeID messageType;
    public bool sequential = false;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>() != null) {
            GameManager.INSTANCE.narrator.TriggerNarrative(messageType, sequential);
        }
    }
}
