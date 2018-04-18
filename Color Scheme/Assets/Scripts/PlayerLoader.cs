using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {

    public GameObject bucket;
    public GameObject flashlight;

    private void Start() {
        Debug.LogWarning("Eyedropper and Laser gun are not implemented in loader");

        bool hasBucket = GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(GameManager.INSTANCE.BUCKET)) != null;
        bool hasFlashlight = GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(GameManager.INSTANCE.FLASHLIGHT)) != null;

        if (hasBucket) {
            GameObject b = Instantiate(bucket, Vector3.zero, new Quaternion());
            PlayerItemPickup pp = b.GetComponent<PlayerItemPickup>();
            pp.Interact();
        }

        if (hasFlashlight) {
            GameObject f = Instantiate(flashlight, Vector3.zero, new Quaternion());
            PlayerItemPickup pp = f.GetComponent<PlayerItemPickup>();
            pp.Interact();
        }

        Destroy(this);
    }
    
}
