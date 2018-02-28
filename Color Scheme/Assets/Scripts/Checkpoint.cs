using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Transform newLocation;

    private void OnTriggerEnter(Collider other) {
        other.GetComponent<Player>().startLocation = newLocation;
    }
}
