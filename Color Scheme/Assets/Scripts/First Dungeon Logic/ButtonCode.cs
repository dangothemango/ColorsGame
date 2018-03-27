using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : ButtonableObject {

    public int codeLength = 5;
    public float flashTime = 1f;
    public Battery battery;
    

    [Header("Code 1")]
    public Color[] code1;
    public Battery[] code1Lights;
    public ButtonActivatedDoor code1Door;

    [Header("Code 2")]
    public Color[] code2;
    public Battery[] code2Lights;
    public ButtonActivatedDoor code2Door;

    int currentCode = 1;
    int codeIndex = 0;
    Color[] currentPresses;
    int pressIndex = 0;
    float t = 0;

    private void Awake() {
        DoAwake();
    }

    protected override void DoAwake() {
        base.DoAwake();
        currentPresses = new Color[codeLength];
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

    protected override void DoStart() {
        base.DoStart();
        battery.Paint(code1[codeIndex]);
    }

    // Update is called once per frame
    void Update () {
        DoUpdate();
	}

    protected override void DoUpdate() {
        base.DoUpdate();
        t += Time.deltaTime;
        if (t > flashTime) {
            t = 0;
            if (currentCode == 1) {
                battery.Paint(code1[(++codeIndex)%codeLength]);
            }
            else {
                battery.Paint(code2[(++codeIndex)%codeLength]);
            }
        }
    }

    public override void OnPressed(Color c) {
        base.OnPressed(c);
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
        OnSuccessfulCode()
    }

    int FindInArray(Color[] arr, Color c) {
        for (int i =0; i<arr.Length; i++) {
            if (arr[i] == c) {
                return i;
            }
        }
        return -1;
    }

    void OnSuccessfulCode() {
        if (currentCode == 1) {
            code1Door.TriggerOpen();
            currentCode++;
        } else if (currentCode == 2) {
            code2Door.TriggerOpen();
        }
    }

}
