using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

    public static Player INSTANCE;
	GameObject PauseMenuBGPanel;
    GameObject CameraOverlay;
	[HideInInspector] public Fuse carriedFuse;
	[HideInInspector] public Graphic hitCameraOverlay;
	[HideInInspector] bool hitByLaser = false;
	[HideInInspector] Color hitColor;

    private bool hasBucket;
    private bool hasFlashlight;
    private bool hasEyedropper;
    private bool hasFuse;

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
	private Shade gazedShade;
    [SerializeField] LayerMask layerMask;
	bool dying = false;

    Camera view;
    RaycastHit reachCast;

    public SpriteRenderer tooltipRenderer;
    public float tooltipOffset = .5f;
	float currTime;

    FirstPersonController controller;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        } else {
            Destroy(this.gameObject);
        }

        view = GetComponentInChildren<Camera>();
        sound = gameObject.GetComponent<AudioSource>();

        view = GetComponentInChildren<Camera>();
        sound = gameObject.GetComponent<AudioSource>();
        controller = GetComponent<FirstPersonController>();

    }

    public void TransitionRooms()
    {
        dying = true;
    }

    void Start() {
        //GetComponentInChildren<Image> ().GetComponent<Graphic> ();
        CameraOverlay = GameObject.Find("Player/FirstPersonCharacter/PlayerUITransitionCanvas/HitImage");
        hitCameraOverlay = CameraOverlay.GetComponent<Image> ().GetComponent<Image> (); 
		Color temp = hitCameraOverlay.color; // apparently one cannot do this directly here is the workaround
		temp.a = 0.0f;
		hitCameraOverlay.color = temp;
		PauseMenuBGPanel = GameObject.Find ("Player/FirstPersonCharacter/PlayerUITransitionCanvas/PauseMenuBGPanel");
        hasBucket = false;
        hasFlashlight = false;
        hasEyedropper = false;
        hasFuse = false;
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
        else if (Input.GetKeyDown(GameManager.INSTANCE.EYEDROPPER))
            setItem(items[2]);

		if (Input.GetKeyDown(GameManager.INSTANCE.INTERACT)) { 
		
            if (!(equippedItem is Flashlight) && equippedItem != null && equippedItem.CanUseOn(gazedObject)) {
				equippedItem.UseOn(gazedObject);
            } else if (gazedObject != null) {
			    gazedObject.Interact();
			} else if (equippedItem is Flashlight) {
                equippedItem.UseOn(gazedObject);
            }
		}

		else if (Input.GetKeyDown(GameManager.INSTANCE.ITEM_SECONDARY) && equippedItem != null)
			equippedItem.SecondaryUsage();

		if (hitByLaser) {
			hitCameraOverlay.color = hitColor;
			Color temp = hitCameraOverlay.color; // apparently one cannot do this directly, temp is the workaround
			temp.a = Mathf.Lerp (0.0f, 1.0f, Time.time / (currTime + 3.0f));
			hitCameraOverlay.color = temp;
		}

		//Pause Game code
		if (Input.GetKeyDown (GameManager.INSTANCE.PAUSE_GAME)) {
			PauseGame ();
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

                Physics.Raycast(view.transform.position, direction.normalized, out reachCast, direction.magnitude, layerMask);
                if (GameManager.INSTANCE.debug) {
                    Debug.DrawLine(view.transform.position, view.transform.position + direction, Color.green);
                }
                if (reachCast.collider != null) {
                    break;
                }
            }
        }

		if (reachCast.collider == null || reachCast.collider.GetComponent<Shade>()==null) {
			if (gazedShade != null) {
				gazedShade.onGazeExit ();
				gazedShade = null;
			}
		} // deal with entry and exit of this code. Do we want to consider what item is equiped or nah.

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
		if (dying)
			return;
		dying = true;
        controller.ToggleDead(true);
        sound.PlayOneShot(deathNoise);
        Invoke("resetPosition", DEATHTIME);
    }

    public void resetPosition()
    {
        if (startLocation == null) return;
        transform.localPosition = startLocation.position;
        transform.localRotation = startLocation.rotation;
        transform.localScale = startLocation.localScale;    // Just covering all bases
		//Color temp = hitCameraOverlay.color;
		//temp.a = 0.0f;
		//hitCameraOverlay.color = temp;
		//hitCameraOverlay.color = Color.clear;
		hitByLaser = false;
		dying = false;
		FilterItems(Color.black);
        controller.ToggleDead(false);

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
        GameManager.INSTANCE.SaveSomething(GameManager.INSTANCE.GetItemSaveString(item.GetType().Name),true.ToString());
		items.Add(item);
        if (item.name == "bucket")
        {
            hasBucket = true;
        }
        else if (item.name == "Flashlight")
        {
            hasFlashlight = true;
        }
        else if (item.name == "Eyedropper")
        {
            hasEyedropper = true;
        }
        else if (carriedFuse != null)
        {
            hasFuse = true;
        }
        configItem(item);
		setItem(item);
	}

    public bool bucket()
    {
        return hasBucket;
    }

    public bool flashlight()
    {
        return hasFlashlight;
    }

    public bool eyedropper()
    {
        return hasEyedropper;
    }

    public bool fuse()
    {
        return hasFuse;
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

	public void PauseGame(){
        if (PauseMenuBGPanel.activeInHierarchy == false) {
			PauseMenuBGPanel.SetActive(true);
            CameraOverlay.SetActive(false);
			Time.timeScale = 0;
			INSTANCE.GetComponent<Transform>().GetComponent<FirstPersonController> ().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        } else {
			PauseMenuBGPanel.SetActive(false);
            CameraOverlay.SetActive(true);
			Time.timeScale = 1;
			INSTANCE.GetComponent<Transform>().GetComponent<FirstPersonController> ().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
