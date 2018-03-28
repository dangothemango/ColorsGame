using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D1MusicManager : MonoBehaviour {
    [SerializeField]
    AudioSource[] musicians;

	// Use this for initialization
	void Start () {
        musicians = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
