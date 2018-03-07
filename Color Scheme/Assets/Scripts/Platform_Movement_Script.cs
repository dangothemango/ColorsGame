using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movement_Script : MonoBehaviour {

    public Transform[] Waypoints;
    public float speed;

    int waypointIndex = 0;

    int direction = 1;
    
    protected Transform attachedObject;

    private void Awake() {
    }

    protected void Update() {
        Vector3 startP = transform.position;
        if (transform.position != Waypoints[waypointIndex%Waypoints.Length].position) {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[waypointIndex%Waypoints.Length].position, speed * Time.deltaTime);
        }
        else {
            waypointIndex+=direction;
            if (waypointIndex < 0) {
                waypointIndex = 0;
                direction = 1;
            }
        }
        Vector3 diff = transform.position - startP;
        if (attachedObject !=null) {
            attachedObject.position += diff;
        }
    }

    public void Bounce() {
        direction *= -1;
        waypointIndex += direction;
    }

    public void Attach(Transform collision) {
        attachedObject=collision;
        Debug.Log("Attaching: " + collision.name);
    }

    public void Detach(Transform collision) { 
        attachedObject=null;
        Debug.Log("Detaching: " + collision.name);
    }
}
