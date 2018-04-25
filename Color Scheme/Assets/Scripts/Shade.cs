using System;
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
    public bool shadeIsInteractedWith = false;

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
        x = UnityEngine.Random.Range(-maxSpeed, maxSpeed);
        y = UnityEngine.Random.Range(-maxSpeed, maxSpeed);
        z = UnityEngine.Random.Range(-maxSpeed, maxSpeed);
        angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        enabled = true;
    }

    public void Update()
    {
        if (enabled && !shadeIsInteractedWith)
        {
            time += Time.deltaTime;

            //if (time > 1.0f)
            //{
                x = UnityEngine.Random.Range(-maxSpeed, maxSpeed);
                z = UnityEngine.Random.Range(-maxSpeed, maxSpeed);
                angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
                //transform.localRotation = Quaternion.Euler(0, angle, 0);
              //  time = 0.0f;
            //}
            if (transform.position.x + x < highX && transform.position.y + y < highY && transform.position.z + z < highZ
                && transform.position.x + x > lowX && transform.position.y + y > lowY && transform.position.z + z > lowZ)
            {
                transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
            }
            else
            {
                deflect();
            }
            if (shadeColor.a < 1.0) {
                StartCoroutine(replenishShade());
            }

        }
    }

    private IEnumerator replenishShade()
    {
        while (shadeColor.a < 1.0 && !shadeIsInteractedWith)
        {
            shadeColor.a += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void deflect()
    {
        if (transform.position.x > highX)
        {
            x = UnityEngine.Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(angle, 0, 0);
        }
        if (transform.position.x < lowX)
        {
            x = UnityEngine.Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(angle, 0, 0);
        }
        if (transform.position.y > highY)
        {
            y = UnityEngine.Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
        if (transform.position.y < lowY)
        {
            y = UnityEngine.Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
        if (transform.position.z > highZ)
        {
            z = UnityEngine.Random.Range(-maxSpeed, 0.0f);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        if (transform.position.z < lowZ)
        {
            z = UnityEngine.Random.Range(0.0f, maxSpeed);
            angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 45;
            //transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
    }

    public IEnumerator changeColor(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(3);
        int index = UnityEngine.Random.Range(0, changeArray.Length);
        obj.GetComponent<PaintableObject>().Paint(changeArray[index]);
        enabled = true;
    }

    public void OnCollisionEnter(Collision col)
    {
        // Shade collided with paintable object
        if (col.gameObject.name == "Battery")
        {
            int rng = UnityEngine.Random.Range(1, 11);
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