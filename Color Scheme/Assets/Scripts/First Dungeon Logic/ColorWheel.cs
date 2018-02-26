using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheel : ButtonableObject {

    public PaintableObject redNode;
    public PaintableObject greenNode;
    public PaintableObject blueNode;

    public EmancipationFilter filter;

    public override void OnPressed(Color c) {
        base.OnPressed(c);
        if (redNode.color == Color.red && greenNode.color == Color.green && blueNode.color == Color.blue) {
            filter.ChangeColor(Color.green);
        }
    }
}
