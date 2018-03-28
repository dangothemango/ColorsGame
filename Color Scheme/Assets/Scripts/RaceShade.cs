using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceShade : SimplePaintableObject, ShadeInterface
{
    Renderer rd;
	bool changingColor;
    public Transform target;
    public float maxSpeed;

    public Color shadeColor;
    public Color change;
    public Color killColor;

    public void Start()
    {
        enabled = true;
    }

    public void Update()
    {
		if (!changingColor)
        {
            float step = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    public IEnumerator changeColor(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(3);
		if (obj.GetComponent<PaintableObject>())
		{
			Debug.Log("DOIFJSOIDFJ");
			obj.GetComponent<PaintableObject>().Paint(change);
		}
		enabled = false;
        //obj.GetComponent<Renderer>().material.color = change;
    }

    public void OnCollisionEnter(Collision col)
    {
        // Shade collided with paintable object
		if (col.gameObject.GetComponent<PaintableObject>() && col.transform == target && !changingColor)
        {
            //enabled = false;
			changingColor = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            StartCoroutine(changeColor(col.gameObject));
        }
        else
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    public void deflect()
    {

    }

    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Destroy(this.gameObject);
        }
    }
}