using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class EmancipationFilter : MonoBehaviour 
{
	[SerializeField] private WaterBase front, back;
	[SerializeField] Color filterColor;

	// Use this for initialization
	void Awake() 
	{
		SetColor();
	}

	public void ChangeColor(Color c)
	{
		if (filterColor == c)
			return;

		filterColor = c;

		SetColor();
	}

	void SetColor()
	{
		foreach (WaterBase w in GetComponentsInChildren<WaterBase>())
		{
			//w.sharedMaterial.SetColor("_BaseColor", new Color(filterColor.r, filterColor.g, filterColor.b, w.sharedMaterial.GetColor("_BaseColor").a));
			//w.sharedMaterial.SetColor("_ReflectionColor", new Color(filterColor.r, filterColor.g, filterColor.b, w.sharedMaterial.GetColor("_ReflectionColor").a));
			Material mat = new Material(w.sharedMaterial);
			mat.SetColor("_BaseColor", new Color(filterColor.r, filterColor.g, filterColor.b, mat.GetColor("_BaseColor").a));
			mat.SetColor("_ReflectionColor", new Color(filterColor.r, filterColor.g, filterColor.b, mat.GetColor("_ReflectionColor").a));
			w.sharedMaterial = mat;
			foreach (WaterTile t in w.GetComponentsInChildren<WaterTile>())
			{
				t.GetComponent<Renderer>().material = mat;
			}
		}
		if (filterColor == Color.black)
			GetComponent<Collider>().isTrigger = false;
		else
			GetComponent<Collider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (filterColor == Color.white)
			return;
		if (other.GetComponent<Player>())
		{
			Player player = other.GetComponent<Player>();
			player.FilterItems(filterColor);
		}
		else if (other.GetComponent<PaintableObject>())
		{
			if (other.GetComponent<PaintableObject>().Color != filterColor)
			{
                if (other.GetComponent<Platform_Movement_Script>()) {
                    other.GetComponent<Platform_Movement_Script>().Bounce();
                }
                else if (other.GetComponent<LightActivatedDoor>()!=null && other.GetComponent<ButtonActivatedDoor>() != null){
                    print("FUCKING DIE " + other);
                    Destroy(other.gameObject);
                    print("TERMINATED");
                }
			}
		}
	}
}
