using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceShade : Shade
{
    Renderer rd;
	bool changingColor;
    public Transform target;
    public float maxSpeed;

    public Color shadeColor;
    public Color change;

    protected override void Start()
    {
        base.Start();
        enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        if (dying) return;

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

}