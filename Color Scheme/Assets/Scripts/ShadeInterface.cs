using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShadeInterface {


    void Start();
    void Update();
    void deflect();
    IEnumerator changeColor(GameObject obj);
    void OnCollisionEnter(Collision col);
}
