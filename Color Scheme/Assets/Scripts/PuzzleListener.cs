using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleListener : MonoBehaviour {

    [SerializeField]
    PaintableObject source;
    [SerializeField]
    Color solution = Color.clear;
	

    void CheckSolution(Color c) {
        if (source != null) {
            if (source.Color == solution) {
                GameManager.INSTANCE.OnPuzzleCompleted(GameManager.PUZZLE_ID.PUZZLE_LISTENER);
                source.paintCallback -= CheckSolution;
                this.enabled = false;
            }
        }
    }

    private void Start() {
        source.paintCallback += CheckSolution;
    }

}
