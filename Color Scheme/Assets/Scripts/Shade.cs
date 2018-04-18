using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : SimplePaintableObject, ShadeInterface
{
    Renderer r;

    public float maxSpeed;

    public float highX;
    public float highY;
    public float highZ;
    public float lowX;
    public float lowY;
    public float lowZ;

    private float x;
    private float y;
    private float z;
    private float time;
    private float angle;
	private bool  highlightShade;

    public Color shadeColor;
    public Color[] changeArray;
    public Color killColor;

    public void Start()
    {
        x = Random.Range(-maxSpeed, maxSpeed);
        y = Random.Range(-maxSpeed, maxSpeed);
        z = Random.Range(-maxSpeed, maxSpeed);
        angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        enabled = true;
    }

    public void Update()
    {
        if (enabled)
        {
            time += Time.deltaTime;

            if (time > 1.0f)
            {
                x = Random.Range(-maxSpeed, maxSpeed);
                z = Random.Range(-maxSpeed, maxSpeed);
                angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
                transform.localRotation = Quaternion.Euler(0, angle, 0);
                time = 0.0f;
            }

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
        }
    }

    public void deflect()
    {
        if (transform.localPosition.x > highX)
        {
            x = Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }
        if (transform.localPosition.x < lowX)
        {
            x = Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }
        if (transform.localPosition.y > highY)
        {
            y = Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(y, x) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }
        if (transform.localPosition.y < lowY)
        {
            y = Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(y, x) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }
        if (transform.localPosition.z > highZ)
        {
            z = Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }
        if (transform.localPosition.z < lowZ)
        {
            z = Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            time = 0.0f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
    }

    public IEnumerator changeColor(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(3);
        int index = Random.Range(0, changeArray.Length);
        obj.GetComponent<Renderer>().material.color = changeArray[index];
        enabled = true;
    }

    public void OnCollisionEnter(Collision col)
    {
        // Shade collided with paintable object
        if (col.gameObject.name == "Cube")
        {
            int rng = Random.Range(1, 11);
            // 70% chance that the shade stops to change object color
            if (rng < 7)
            {
                enabled = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                StartCoroutine(changeColor(col.gameObject));
            }
            deflect();
        }
        // Shade collided with wall
        else if (col.gameObject.name == "Wall")
        {
            deflect();
        }
        else
        {
            // Shade passes through object
            if (col.gameObject.name == "Cylinder")
            {
                Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }

    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Destroy(this.gameObject);
        }
    }

	public void onGazeExit(){
		// stop shade emitting particles or glowing
		highlightShade = false;
	}
}