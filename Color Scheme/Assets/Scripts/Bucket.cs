using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : PlayerItem 
{
	[SerializeField] private SimplePaintableObject paint;
	bool hasPaint = false;

	// Use this for initialization
	void Start() 
	{
		if (!paint)
			paint = GetComponentInChildren<SimplePaintableObject>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		
	}

	private void UsePaint(PaintableObject target)
	{
		target.Paint(paint.color);
		hasPaint = false;
		paint.gameObject.SetActive(false);
	}

	private void FillBucket(Color c)
	{
		if (!hasPaint)
		{
			hasPaint = true;
			paint.gameObject.SetActive(true);
		}

		paint.Paint(c);
	}

	public override bool CanUseOn(InteractableObject target)
	{
		return (target is PaintFountain) || (hasPaint && target.GetComponent<PaintableObject>());
	}

	public override void UseOn(InteractableObject target)
	{
		if (target is PaintFountain)
		{
			
		}
		else if (hasPaint)
		{
			PaintableObject surface = target.GetComponent<PaintableObject>();
			if (surface)
			{
				UsePaint(surface);
			}
			else
			{
				throw new System.Exception("WHAT THE ACTUAL FU-");
			}
		}
	}
}
