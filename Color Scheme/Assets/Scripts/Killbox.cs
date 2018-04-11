using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ReverseNormals))]
public class Killbox : MonoBehaviour {

    ReverseNormals rv;

	// Use this for initialization
	void Start () {
        rv = GetComponent<ReverseNormals>();
        rv.InvertNormals();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Die");
        other.gameObject.SendMessage("die", false);
        rv.InvertNormals();
    }

    private void OnTriggerExit(Collider other) {
        rv.InvertNormals();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, 2 * transform.localScale);
    }
}
