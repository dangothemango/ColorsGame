using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCode : ButtonableObject {

    public int codeLength = 5;
    public float flashTime = 1f;
    public Battery battery;

    [Header("Code 1")]
    public Color[] code1;
    public Battery[] code1Lights;
    public ButtonActivatedDoor code1Door;

    [Header("Transition")]
    public Color newColor;

    [Header("Code 2")]
    public Color[] code2;
    public Battery[] code2Lights;
    public ButtonActivatedDoor code2Door;

    [Header("Other Attributes")]
    public Texture[] cookies;
    public Renderer[] codeLabels;
    public Light codeLight;

    int currentCode = 1;
    int codeIndex = 0;
    Color[] currentPresses;
    int pressIndex = 0;
    float t = 0;
    bool updateCode = true;
    string saveString;

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
        saveString = SceneManager.GetActiveScene().name + gameObject.name + "Code";
        currentPresses = new Color[codeLength];
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected override void DoStart() {
        base.DoStart();
        battery.Paint(code1[codeIndex]);
        string loadedCode = GameManager.INSTANCE.LoadSomething(saveString);
        if (loadedCode != null) {
            currentCode = int.Parse(loadedCode);
        }
        codeLight.cookie = currentCode <= cookies.Length ? cookies[currentCode-1] : null; 
        for (int i =0; i < currentCode-1; i++) {
            Renderer r = codeLabels[i];
            r.material.SetColor("_MainColor", Color.green);
            r.material.SetColor("_EmissionColor", Color.green);
        }
    }

    // Update is called once per frame
    void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {
        base.DoUpdate();
        t += Time.deltaTime;
        if (updateCode && t > flashTime) {
            t = 0;
            switch (currentCode) {
                case 1:
                    battery.Paint(code1[(++codeIndex)%codeLength]);
                    break;
                case 2:
                    battery.Paint(code2[(++codeIndex)%codeLength]);
                    break;
                case 3:
                    battery.Paint(Color.green);
                    this.enabled = false;
                    break;
            }
        }
    }

    public override void OnPressed(Color c) {
        if (!updateCode) return;
        base.OnPressed(c);
        switch (currentCode) {
            case 1:
                code1Lights[pressIndex % codeLength].Paint(c);
                break;
            case 2:
                code2Lights[pressIndex % codeLength].Paint(c);
                break;
        }
        currentPresses[(pressIndex++)%codeLength] = c;

        if (pressIndex >= codeLength) {
            checkCode();
        }
    }

    void checkCode() {
        int pressesItr = pressIndex+1;
        Color[] code = currentCode == 1 ? code1 : code2;
        int codeItr = FindInArray(code,currentPresses[pressesItr%codeLength]);
        if (codeItr == -1) {
            return;
        }
        for (int i=0; i<codeLength; i++) {
            if (currentPresses[(pressesItr++) % codeLength]!=(code[(codeItr++)%codeLength])) {
                return;
            }
        }
        OnSuccessfulCode();
    }

    int FindInArray(Color[] arr, Color c) {
        for (int i =0; i<arr.Length; i++) {
            if (arr[i] == c) {
                return i;
            }
        }
        return -1;
    }

    IEnumerator FlashNewColor() {
        updateCode = false;
        int count = 0;
        while (count < 6) {
            battery.Paint(newColor);
            yield return new WaitForSeconds(.3f);
            battery.Paint(Color.black);
            yield return new WaitForSeconds(.3f);
            count++;
        }
        updateCode = true;
    }

    void OnSuccessfulCode() {
        if (currentCode <= codeLabels.Length) {
            Renderer r = codeLabels[currentCode-1];
            r.material.SetColor("_MainColor", Color.green);
            r.material.SetColor("_EmissionColor", Color.green);
        }
        switch (currentCode) {
            case 1:
                code1Door.TriggerOpen();
                foreach (Battery b in code1Lights) {
                    b.Paint(Color.green);
                }
                StartCoroutine(FlashNewColor());
                break;
            case 2:
                code2Door.TriggerOpen();
                foreach (Battery b in code2Lights) {
                    b.Paint(Color.green);
                }
                break;
        }
        currentCode++;
        codeLight.cookie = currentCode <= cookies.Length ? cookies[currentCode - 1] : null;
        GameManager.INSTANCE.SaveSomething(saveString, currentCode.ToString());
        pressIndex = 0;
        currentPresses = new Color[codeLength];
        GameManager.INSTANCE.OnPuzzleCompleted(GameManager.PUZZLE_ID.BUTTON_CODE);
    }

}
