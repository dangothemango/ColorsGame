using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : PlayerItem 
{
	[SerializeField] private SimplePaintableObject paint;

	Color currentColor;
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
		target.Paint(currentColor);
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
		currentColor = c;
	}

	public override bool CanUseOn(InteractableObject target)
	{
		return (target is PaintFountain) || (hasPaint && target.GetComponent<PaintableObject>());
	}

	public override void UseOn(InteractableObject target)
	{
		if (target is PaintFountain)
		{
			PaintFountain p = target as PaintFountain;
			FillBucket(p.col);

			// TODO: Uncomment this once PaintFountain.Interact() is implemented.
			//p.Interact();
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

	public override void Filter(Color c)
	{
		
	}
}
