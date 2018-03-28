using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPShade : SimplePaintableObject, ShadeInterface
{
    public Transform platform;
    public float maxSpeed;

    public Color shadeColor;
    public Color[] changeArray;
    public Color killColor;

    public bool called;

    public void Start()
    {
        called = false;
    }

    public void Update()
    {
        while (called)
        {
            StartCoroutine(changeColor(platform.gameObject));
        }
    }

    public IEnumerator changeColor(GameObject obj)
    {
        called = false;
        yield return new WaitForSecondsRealtime(0);
        int index = Random.Range(0, changeArray.Length);
        obj.GetComponent<Renderer>().material.color = changeArray[index];
        called = false;
    }

    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Destroy(this.gameObject);
        }
    }

    public void deflect() { }
    public void OnCollisionEnter(Collision col) { }
}