using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movement_Script : MonoBehaviour {

    public Transform[] Waypoints;
    public float speed;

    int waypointIndex = 0;

    int direction = 1;
    
    List<Transform> attachedObjects;

    private void Awake() {
        attachedObjects = new List<Transform>();
    }

    private void Update() {
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
        foreach (Transform t in attachedObjects) {
            t.position += diff;
        }
    }

    public void Bounce() {
        direction *= -1;
        waypointIndex += direction;
    }

    private void OnCollisionEnter(Collision collision) {
        attachedObjects.Add(collision.transform);
        Debug.Log("Attaching: " + collision.transform.name);
    }

    private void OnCollisionExit(Collision collision) { 
        attachedObjects.Remove(collision.transform);
        Debug.Log("Attaching: " + collision.transform.name);
    }
}
