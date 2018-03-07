using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintableObject : ButtonableObject {

<<<<<<< HEAD
    public bool botherSaving = true;
    public bool debugObject = false;

    [SerializeField]
    Color color;

    public Color Color {
        get {
            return color;
        }
        set {
            color = value;
            if (botherSaving)
                SaveColor(color);
        }
    }

    string saveString;
=======
    public Color color;
	public bool colorChanges = false;
>>>>>>> ef16703b421ebd687cd4f28d53528015f5451144
    
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
<<<<<<< HEAD
        TryLoadColor();
        Paint(Color);
=======
		
>>>>>>> ef16703b421ebd687cd4f28d53528015f5451144
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
        Color o;
        ColorUtility.TryParseHtmlString(savedColor, out o);
        if (o == null) {
            return false;
        }
        color = o;
        return true;
    }

    void SaveColor(Color c) {
        GameManager.INSTANCE.SaveSomething(saveString, "#"+ColorUtility.ToHtmlStringRGBA(c));
    }
}
