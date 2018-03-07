using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player INSTANCE;
	[HideInInspector] public Fuse carriedFuse;

    [SerializeField] float DEATHTIME = 2.3f;

	[SerializeField] List<PlayerItem> items = new List<PlayerItem>();
	PlayerItem equippedItem = null;

    public Transform startLocation;
    AudioSource sound;
    [SerializeField] AudioClip deathNoise;

    [Header("Interaction Config")]
    [SerializeField]
    float reachDistance = 5f;
    [SerializeField]
    float coneRadius = .2f;
    [SerializeField]
    float stepSize = Mathf.PI/6;
    [SerializeField] InteractableObject gazedObject;
    [SerializeField] LayerMask layerMask;

    Camera view;
    RaycastHit reachCast;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        } else {
            Destroy(this.gameObject);
        }
    }


    void Start() {
        view = GetComponentInChildren<Camera>();
        sound = gameObject.GetComponent<AudioSource>();
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

		if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT)) { 
		
            if (equippedItem != null && equippedItem.CanUseOn(gazedObject)) {
                equippedItem.UseOn(gazedObject);
            } else if (gazedObject != null) {
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
        Physics.Raycast(view.transform.position, view.transform.forward, out reachCast, reachDistance, layerMask);
        if (GameManager.INSTANCE.debug) {
            Debug.DrawLine(view.transform.position, view.transform.position + view.transform.forward * reachDistance, Color.green);
        }
        if (reachCast.collider == null) {
            for (float angle = 0; angle < Mathf.PI * 2; angle += stepSize) {
                Vector3 direction = (view.transform.forward*reachDistance) + (view.transform.right*Mathf.Cos(angle) + view.transform.up * Mathf.Sin(angle)).normalized*coneRadius;

                Physics.Raycast(view.transform.position, direction.normalized, out reachCast, direction.magnitude,layerMask);
                if (GameManager.INSTANCE.debug) {
                    Debug.DrawLine(view.transform.position, view.transform.position + direction, Color.green);
                }
                if (reachCast.collider != null) {
                    break;
                }
            }
        }

        if (reachCast.collider == null ||  reachCast.collider.GetComponent<InteractableObject>()==null) {
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
        // if(fallOver)
        // {
            // TODO: Manipulate rotation to make the player fall over
        // }
        sound.PlayOneShot(deathNoise);
        Invoke("resetPosition", DEATHTIME);
        
    }

    public void resetPosition()
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
        item.transform.SetParent(view.gameObject.transform);
        item.transform.localScale = Vector3.one * item.itemScale;
        item.transform.localRotation = Quaternion.Euler(item.itemRotation);
        item.transform.localPosition = item.itemOffset;
    }
}
