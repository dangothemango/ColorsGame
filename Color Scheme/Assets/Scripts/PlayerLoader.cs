﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {

    public GameObject bucket;
    public GameObject flashlight;
    public GameObject eyedropper;

    private void Start() {
        Debug.LogWarning("Laser gun are not implemented in loader");

        bool hasBucket = GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(typeof(Bucket).Name)) != null;
        bool hasFlashlight = GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(typeof(Flashlight).Name)) != null;
        bool hasEyedropper = GameManager.INSTANCE.LoadSomething(GameManager.INSTANCE.GetItemSaveString(typeof(EyedropperScript).Name)) != null; 

        if (hasBucket) {
            GameObject b = Instantiate(bucket, Vector3.zero, new Quaternion());
            PlayerItemPickup pp = b.GetComponent<PlayerItemPickup>();
            pp.Interact();
        }

        if (hasFlashlight) {
            Debug.Log("Loading Flashlight");
            GameObject f = Instantiate(flashlight, Vector3.zero, new Quaternion());
            PlayerItemPickup pp = f.GetComponent<PlayerItemPickup>();
            pp.Interact();
        }

        if (hasEyedropper)
        {
            GameObject e = Instantiate(eyedropper, Vector3.zero, new Quaternion());
            PlayerItemPickup pp = e.GetComponent<PlayerItemPickup>();
            pp.Interact();
        }
        Destroy(this);
    }
    
}
