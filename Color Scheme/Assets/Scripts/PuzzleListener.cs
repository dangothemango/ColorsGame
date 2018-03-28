using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleListener : MonoBehaviour {

    [SerializeField]
    PaintableObject source;
    [SerializeField]
    Color solution = Color.clear;
	
	// Update is called once per frame
	void Update () {
        if (source != null) {
            if (source.Color == solution) {
                GameManager.INSTANCE.OnPuzzleCompleted(GameManager.PUZZLE_ID.PUZZLE_LISTENER);
                this.enabled = false;
            }
        }
    }
    
}
