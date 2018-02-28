using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour {

    public Platform_Movement_Script movingScript;


    private void OnTriggerEnter(Collider other) {
        movingScript.Attach(other.transform);
    }

    private void OnTriggerExit(Collider other) {
        movingScript.Detach(other.transform);
    }
}
