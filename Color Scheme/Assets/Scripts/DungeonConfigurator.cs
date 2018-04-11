using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonConfigurator : MonoBehaviour {

    public Transform[] playerSpawns;
    public int dungeonID = 0;

	// Use this for initialization
	void Start () {
        Player.INSTANCE.startLocation = playerSpawns[GameManager.INSTANCE.currentDungeon.dungeonID];
        Player.INSTANCE.resetPosition();
        GameManager.INSTANCE.currentDungeon = this;
	}
}
