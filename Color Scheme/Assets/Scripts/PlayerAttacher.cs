using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour {

    public Platform_Movement_Script movingScript;

    private void OnTriggerEnter(Collider other) {
        Attach(other.transform);
    }

    private void OnTriggerExit(Collider other) {
        Detach(other.transform);
    }

	protected virtual void Attach(Transform other)
	{
		movingScript.Attach(other);
	}

	protected virtual void Detach(Transform other)
	{
		movingScript.Detach(other);
	}
}
