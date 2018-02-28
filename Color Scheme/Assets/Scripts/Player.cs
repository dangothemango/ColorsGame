﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float reachDistance = 5f;
    public InteractableObject gazedObject;

	[SerializeField] List<PlayerItem> items = new List<PlayerItem>();
	PlayerItem equippedItem = null;

    [SerializeField] private Transform startLocation;
    [SerializeField]
    private Transform cameraTransform;

    Camera view;
    RaycastHit reachCast;

    [SerializeField] private AudioSource sound;

    void Awake() {

    }


    void Start() {
        view = GetComponentInChildren<Camera>();
		if (GameManager.INSTANCE.playerInstance == null)
		{
			GameManager.INSTANCE.playerInstance = this;
			resetPosition();
		}
		else
		{
			GameManager.INSTANCE.playerInstance.startLocation = this.startLocation;
			GameManager.INSTANCE.playerInstance.resetPosition();
			Destroy(gameObject);
		}
        // sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        calcView();

        if (equippedItem != null) {
            configItem(equippedItem);
        }
		if (Input.GetKeyDown(GameManager.INSTANCE.NO_ITEM))
			setItem(null);
		else if (Input.GetKeyDown(GameManager.INSTANCE.BUCKET))
		{
//			foreach (PlayerItem i in items)
//			{
//				if (Input.GetKeyDown(i.itemKey))
//				{
//					setItem(i);
//					break;
//				}
//			}
			setItem(items[0]);
		}

		if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT))
		{
			if (gazedObject != null)
			{
				if (equippedItem != null && equippedItem.CanUseOn(gazedObject))
					equippedItem.UseOn(gazedObject);
				else
					gazedObject.Interact();
			}
		}
		else if (Input.GetKeyDown(GameManager.INSTANCE.ITEM_SECONDARY) && equippedItem != null)
			equippedItem.SecondaryUsage();
		
    }

	public void FilterItems(Color c)
	{
		foreach (PlayerItem i in items)
		{
			i.Filter(c);
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
            gazedObject.onGazeEnter(equippedItem);
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
        if (startLocation == null) return;
        transform.localPosition = startLocation.position;
        transform.localRotation = startLocation.rotation;
        transform.localScale = startLocation.localScale;    // Just covering all bases
    }

	// Change currently equipped item.
	void setItem(PlayerItem item)
	{
		if (equippedItem == item)
			return;

		if (equippedItem != null)
		{
			equippedItem.gameObject.SetActive(false);
		}
			

		if (item != null)
		{
			item.gameObject.SetActive(true);
		}

		equippedItem = item;
	}

	public void addItem(PlayerItem item)
	{
		items.Add(item);
        configItem(item);
		setItem(item);
	}

    void configItem(PlayerItem item) {
        item.transform.SetParent(cameraTransform);
        item.transform.localScale = Vector3.one * item.itemScale;
        item.transform.localRotation = Quaternion.Euler(item.itemRotation);
        item.transform.localPosition = item.itemOffset;
    }
}
