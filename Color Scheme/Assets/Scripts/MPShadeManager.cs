﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPShadeManager : MonoBehaviour
{

    public MPShade[] shades;

    void Start()
    {
        InvokeRepeating("changeColor", 0.0f, 3.0f);
    }

    void changeColor()
    {
        int index = Random.Range(0, shades.Length);
        shades[index].called = true;
        shades[index].Update();
    }

    void Update()
    {

    }

}