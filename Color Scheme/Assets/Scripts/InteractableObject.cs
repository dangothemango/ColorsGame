using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    protected Renderer r;
    protected Material outline;

    [Header("Outline Helpers")]
    public GameObject outlinedMesh;
    public float outlineWidth = .2f;

    

    [Header("Interaction Stuff")]
    public bool interactable = true;
    public Sprite tooltipIcon;

    private void Awake() {
        DoAwake();
    }

    protected virtual void DoAwake() {}

	// Use this for initialization
	void Start () {
        DoStart();
	}

    protected virtual void DoStart() {
        if (outlinedMesh) { 
            r = outlinedMesh.GetComponent<Renderer>();
        }
        else {
            r = GetComponent<Renderer>();
        }
        if (r) {
            foreach (Material m in r.materials) {
                if (m.name.ToLower().Contains("outline")) {
                    outline = m;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected virtual void DoUpdate() {}

    public virtual void onGazeEnter(PlayerItem currentItem) {
        if (outline) {
            outline.SetFloat("_Outline", outlineWidth);
        }
    }

    public virtual void onGazeExit() {
        if (outline) {
            outline.SetFloat("_Outline", 0f);
        }
    }

    public virtual void Interact() {
        throw new System.NotImplementedException("Function must be overridden");
    }
}
