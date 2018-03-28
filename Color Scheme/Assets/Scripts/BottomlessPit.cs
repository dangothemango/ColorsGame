using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ReverseNormals))]
public class BottomlessPit : MonoBehaviour {

    ReverseNormals rv;
   
	// Use this for initialization
	void Start () {
        rv = GetComponent<ReverseNormals>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Throwable" || col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
            rv.InvertNormals();
        }
    }

    void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "Throwable" || col.gameObject.tag == "Player") {
            rv.InvertNormals();
        }
    }
}
