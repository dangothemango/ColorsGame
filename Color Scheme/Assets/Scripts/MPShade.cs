using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPShade : Shade
{
	public PaintableObject target;
    public float maxSpeed;

    public Color[] changeArray;
    

    public bool called;

    protected override void Start()
    {
        base.Start();
        called = false;
    }

    protected override void Update()
    {
        base.Update();
        if (dying) return;

        while (called)
        {
            StartCoroutine(changeColor(target.gameObject));
        }
    }

    public IEnumerator changeColor(GameObject obj)
    {
        called = false;
        yield return new WaitForSecondsRealtime(0);
        int index = Random.Range(0, changeArray.Length);
		obj.GetComponent<PaintableObject>().Paint(changeArray[index]);
        called = false;
    }
}