using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudio : MonoBehaviour {
    AudioSource drone;

	// Use this for initialization
	void Start () {
        drone = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player.INSTANCE.gameObject)
        {
            drone.loop = false;
        }
    }
}
