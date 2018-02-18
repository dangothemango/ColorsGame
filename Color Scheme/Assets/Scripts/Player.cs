using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float reachDistance = 5f;
    public InteractableObject gazedObject;


    Camera view;
    RaycastHit reachCast;

    void Awake() {

    }


    void Start() {
        view = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {
        calcView();
        if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT)) {
            if (gazedObject != null) {
                gazedObject.Interact();
            }
        }
    }

    void calcView() {
        Physics.Raycast(view.transform.position, view.transform.forward, out reachCast, reachDistance);
        if (GameManager.INSTANCE.debug) {
            Debug.DrawLine(view.transform.position, view.transform.position + view.transform.forward * reachDistance, Color.green);
        }
        if (reachCast.collider == null) {
            if (gazedObject != null) {
                gazedObject.onGazeExit();
                gazedObject = null;
            }
            return;
        }
        if (gazedObject != null) {
            if (reachCast.collider.gameObject == gazedObject.gameObject) {
                return;
            }
            gazedObject.onGazeExit();
        }
        gazedObject = reachCast.collider.GetComponent<InteractableObject>();
        if (gazedObject) {
            gazedObject.onGazeEnter();
        }
    }
}
