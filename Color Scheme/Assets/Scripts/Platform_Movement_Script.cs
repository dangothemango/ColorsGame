using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movement_Script : MonoBehaviour {

    public Transform[] Waypoints;
    public float speed;

    int waypointIndex = 0;

    int direction = 1;

    private void Update() {
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
    }

    public void Bounce() {
        direction *= -1;
        waypointIndex += direction;
    }
}
