﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float reachDistance = 5f;
    public InteractableObject gazedObject;

	[SerializeField] List<PlayerItem> items = new List<PlayerItem>();
	PlayerItem equippedItem = null;
	PlayerItem equippedInstance = null;

    [SerializeField] private Transform startLocation;

    Camera view;
    RaycastHit reachCast;

    [SerializeField] private AudioSource sound;

    void Awake() {

    }


    void Start() {
        view = GetComponentInChildren<Camera>();
        // sound = gameObject.GetComponent<AudioSource>();
        resetPosition();
    }

    // Update is called once per frame
    void Update() {
        calcView();

		if (Input.GetKeyDown(GameManager.INSTANCE.NO_ITEM))
			setItem(null);
		else
		{
			foreach (PlayerItem i in items)
			{
				if (Input.GetKeyDown(i.itemKey))
				{
					setItem(i);
					break;
				}
			}
		}

        if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT)) {
            if (gazedObject != null) {
				if (equippedInstance != null && equippedInstance.CanUseOn(gazedObject))
					equippedInstance.UseOn(gazedObject);
				else
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

    public void die(bool fallOver) {
        if(fallOver)
        {
            // TODO: Manipulate rotation to make the player fall over
        }

        sound.Play();
        Invoke("resetPosition", 7.5f);
        
    }

    void resetPosition()
    {
        transform.localPosition = startLocation.position;
        transform.localRotation = startLocation.rotation;
        transform.localScale = startLocation.localScale;    // Just covering all bases
    }

	// Change currently equipped item.
	void setItem(PlayerItem item)
	{
		if (equippedItem == item)
			return;

		if (equippedInstance)
			Destroy(equippedInstance.gameObject);

		if (item != null)
		{
			equippedInstance = Instantiate(item.gameObject, transform).GetComponent<PlayerItem>();
			equippedInstance.transform.localPosition = equippedInstance.itemOffset;
		}

		equippedItem = item;
	}

	public void addItem(PlayerItem item)
	{
		
	}
}
