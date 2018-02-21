using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class EmancipationFilter : MonoBehaviour 
{
	[SerializeField] private WaterBase front, back;
	[SerializeField] Color filterColor;

	// Use this for initialization
	void Start () 
	{
		foreach (WaterBase w in GetComponentsInChildren<WaterBase>())
		{
			w.sharedMaterial.SetColor("_BaseColor", new Color(filterColor.r, filterColor.g, filterColor.b, w.sharedMaterial.GetColor("_BaseColor").a));
			w.sharedMaterial.SetColor("_ReflectionColor", new Color(filterColor.r, filterColor.g, filterColor.b, w.sharedMaterial.GetColor("_ReflectionColor").a));
			//foreach (WaterTile t in w.GetComponentsInChildren<WaterTile>())
			//{
			//	Material mat = t.GetComponent<Renderer>().material;
			//	mat.SetColor("_BaseColor", new Color(filterColor.r, filterColor.g, filterColor.b, mat.GetColor("_BaseColor").a));
			//	mat.SetColor("_ReflectionColor", new Color(filterColor.r, filterColor.g, filterColor.b, mat.GetColor("_ReflectionColor").a));
			//}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
