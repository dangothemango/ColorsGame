using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPShadeManager : SimplePaintableObject
{
    Renderer r;
    public Transform platform;
    public float maxSpeed;

    public Color shadeColor;
    public Color[] changeArray;
    public Color killColor;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        while (enabled)
        {
            StartCoroutine(changeColor(platform.gameObject));
        }
    }

    IEnumerator changeColor(GameObject obj)
    {
        enabled = false;
        yield return new WaitForSecondsRealtime(6);
        int index = Random.Range(0, changeArray.Length);
        obj.GetComponent<Renderer>().material.color = changeArray[index];
        enabled = true;
    }

    public override void Paint(Color c)
    {
        if (c == killColor)
        {
            Destroy(this.gameObject);
        }
    }
}