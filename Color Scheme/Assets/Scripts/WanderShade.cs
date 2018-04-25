using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderShade : Shade
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
    public Color[] changeArray;

    protected override void Start()
    {
        base.Start();
        x = Random.Range(-maxSpeed, maxSpeed);
        y = Random.Range(-maxSpeed, maxSpeed);
        z = Random.Range(-maxSpeed, maxSpeed);
        angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        if (enabled && !shadeIsInteractedWith && !dying)
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
            if (shadeColor.a < 1.0)
            {
                StartCoroutine(replenishShade());
            }

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
}