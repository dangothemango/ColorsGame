using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonConfigurator : MonoBehaviour {

    public Transform playerSpawn;

	// Use this for initialization
	void Start () {
        Player.INSTANCE.startLocation = playerSpawn;
        Player.INSTANCE.resetPosition();
	}
}
