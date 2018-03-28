using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheel : MonoBehaviour {

    public PaintableObject redNode;
    public PaintableObject greenNode;
    public PaintableObject blueNode;

    public EmancipationFilter filter;

    private void Start() {
        InvokeRepeating("CheckSolution", 1f, 1f);
    }

    public void CheckSolution () {
        if (redNode.Color == Color.red && greenNode.Color == Color.green && blueNode.Color == Color.blue) {
            filter.ChangeColor(Color.green);
            GameManager.INSTANCE.OnPuzzleCompleted(GameManager.PUZZLE_ID.COLOR_WHEEL);
            CancelInvoke();
        }
    }
}
