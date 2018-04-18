using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour {

    [SerializeField]
    int sceneNumber;

    private void OnTriggerEnter(Collider other) {
        GameManager.INSTANCE.LoadScene(sceneNumber+1);
    }
}
