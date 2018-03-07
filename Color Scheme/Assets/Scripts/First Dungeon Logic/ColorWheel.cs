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
        if (redNode.Color == Color.red && greenNode.Color == Color.green && blueNode.Color == Color.blue) {
            filter.ChangeColor(Color.green);
        }
    }
}
