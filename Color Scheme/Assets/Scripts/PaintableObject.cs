using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintableObject : ButtonableObject {

    public bool botherSaving = true;
    public bool debugObject = false;

    [SerializeField]
    Color color;

    public Color Color {
        get {
            return color;
        } private set {
            color = value;
            if (botherSaving)
                SaveColor(color);
        }
    }

    string saveString;
    
    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        saveString = SceneManager.GetActiveScene().name + transform.position.ToString() + transform.rotation.ToString() + Color.ToString();     
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected override void DoStart() {
        TryLoadColor();
        Paint(Color);
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {}

    public virtual void Paint(Color c) {
        Color = c;
    }

    public override void OnPressed(Color c) {
        base.OnPressed(c);
        this.Paint(c);
    }

    bool TryLoadColor() {
        string savedColor = GameManager.INSTANCE.LoadSomething(saveString);
        if (savedColor == null) {
            return false;
        }
        Color o = Color.clear;
        ColorUtility.TryParseHtmlString(savedColor, out o);
        if (o == Color.clear) {
            return false;
        }
        color = o;
        return true;
    }

    void SaveColor(Color c) {
        GameManager.INSTANCE.SaveSomething(saveString, "#"+ColorUtility.ToHtmlStringRGBA(c));
    }
}
