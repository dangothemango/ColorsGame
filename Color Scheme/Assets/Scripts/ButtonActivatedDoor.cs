using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActivatedDoor : ButtonableObject {

    public float openTime = 1f;
    public GameObject mesh;

    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;

    float startY;
    Coroutine doorMovement;
    new Collider collider;
    AudioSource sound;

    string saveString;

	public override void OnPressed(Color c)
	{
		TriggerOpen();
	}

    private void Awake() {
        DoAwake();
    }

    private void Start() {
        DoStart();
    } 

    protected override void DoStart() {
        base.DoStart();
        saveString = SceneManager.GetActiveScene().name + transform.position.ToString() + transform.rotation.ToString() + "ButtonActivatedDoor";
        startY = mesh.transform.localPosition.y;
        collider = GetComponent<Collider>();
        sound = GetComponent<AudioSource>();
        string state = GameManager.INSTANCE.LoadSomething(saveString);
        if (state == "open") {
            TriggerOpen();
        }
    }

    private void Update() {
        DoUpdate();
    }

    public void TriggerOpen() {
        GameManager.INSTANCE.SaveSomething(saveString, "open");
        if (doorMovement == null) {
            doorMovement = StartCoroutine(Open());
        }
        
    }

    public void TriggerClose() {
        GameManager.INSTANCE.SaveSomething(saveString, "closed");
        if (doorMovement == null) {
            doorMovement = StartCoroutine(Close());
        }
        
    }

    IEnumerator Open() { 
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, -startY, o.z);
        sound.clip = open;
        sound.Play();
        while (t < openTime) {
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        collider.enabled = false;
        doorMovement = null;
    }

    IEnumerator Close() {
        collider.enabled = true;
        float t = 0;
        Vector3 o = mesh.transform.localPosition;
        Vector3 d = new Vector3(o.x, startY, o.z);
        sound.clip = close;
        sound.Play();
        while (t < openTime) {
            mesh.transform.localPosition = Vector3.Lerp(o, d, t / openTime); ;
            yield return null;
            t += Time.deltaTime;
        }
        mesh.transform.localPosition = d;
        doorMovement = null;
    }

}

