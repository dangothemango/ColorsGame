using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPShadeManager : MonoBehaviour
{

    public MPShade[] shades;

    void Start()
    {

    }

    void Update()
    {
        int index = Random.Range(0, shades.Length);
        shades[index].called = true;
    }

}