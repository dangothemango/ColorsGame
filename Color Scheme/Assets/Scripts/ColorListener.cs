using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorListener : MonoBehaviour {

    [SerializeField]
    PaintableObject source;
    [SerializeField]
    PaintableObject target;

    private void Awake() {
        if (target == null) {
            target = GetComponent<PaintableObject>();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (source != null && target != null) {
            if (source.Color != target.Color) {
                target.Paint(source.Color);
            }
        }
	}
}
