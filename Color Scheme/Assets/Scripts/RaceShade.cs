using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceShade : SimplePaintableObject
{
    Renderer rd;
    public Transform target;
    public float maxSpeed;

    public Color shadeColor;
    public Color change;
    public Color killColor;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        if (enabled)
        {
            float step = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    IEnumerator changeColor(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(3);
		if (obj.GetComponent<PaintableObject>())
			obj.GetComponent<PaintableObject>().Paint(change);
        //obj.GetComponent<Renderer>().material.color = change;
    }

    void OnCollisionEnter(Collision col)
    {
        // Shade collided with paintable object
        if (col.gameObject.name == "PointLight")
        {
            enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            StartCoroutine(changeColor(col.gameObject));
        }
        else
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Destroy(this.gameObject);
        }
    }
}