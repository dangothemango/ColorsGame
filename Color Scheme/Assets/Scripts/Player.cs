using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player INSTANCE;
	[HideInInspector] public Fuse carriedFuse;
	[HideInInspector] public Graphic hitCameraOverlay;
	[HideInInspector] bool hitByLaser = false;
	[HideInInspector] Color hitColor;

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

    public SpriteRenderer tooltipRenderer;
    public float tooltipOffset = .5f;
	float currTime;

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
		hitCameraOverlay = GetComponentInChildren<Image> ().GetComponent<Graphic> ();
		Color temp = hitCameraOverlay.color; // apparently one cannot do this directly here is the workaround
		temp.a = 0.0f;
		hitCameraOverlay.color = temp;
    }

    // Update is called once per frame
    void Update() {
        calcView();
        ConfigureTooltip();

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
		else if (Input.GetKeyDown(GameManager.INSTANCE.FLASHLIGHT))
			setItem(items[1]);

		if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT)) { 
		
            if (equippedItem != null && equippedItem.CanUseOn(gazedObject)) {
                equippedItem.UseOn(gazedObject);
            } else if (gazedObject != null) {
			    gazedObject.Interact();
			}
		}

		else if (Input.GetKeyDown(GameManager.INSTANCE.ITEM_SECONDARY) && equippedItem != null)
			equippedItem.SecondaryUsage();

		if (hitByLaser) {
			hitCameraOverlay.color = hitColor;
			Color temp = hitCameraOverlay.color; // apparently one cannot do this directly here is the workaround
			temp.a = Mathf.Lerp (0.0f, 1.0f, Time.time / (currTime + 3.0f));
			hitCameraOverlay.color = temp;
		}
		
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

        if (reachCast.collider == null || reachCast.collider.GetComponent<InteractableObject>()==null) {
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

    void ConfigureTooltip() {
        if (gazedObject == null) {
            tooltipRenderer.sprite = null;
            return;
        }
        Color c = Color.white;
        tooltipRenderer.sprite = equippedItem == null || !equippedItem.CanUseOn(gazedObject) ?  gazedObject.tooltipIcon : equippedItem.GetTooltipIcon(gazedObject, out c);
        Vector3 p = tooltipRenderer.transform.localPosition;
        p.z = (reachCast.point - view.transform.position).magnitude-tooltipOffset;
        tooltipRenderer.transform.localPosition = p;
        tooltipRenderer.color = c;
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
		Color temp = hitCameraOverlay.color; // apparently one cannot do this directly here is the workaround
		temp.a = 0.0f;
		hitCameraOverlay.color = temp;
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

	public void hitByLaserTrigger(Color c){
		hitByLaser = true;
		hitColor = c;
		currTime = Time.time;
	}
}
