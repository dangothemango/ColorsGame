using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : PlayerItem 
{
	[SerializeField] private SimplePaintableObject paint;
    private AudioSource splish;

	public Color currentColor = Color.clear;
	bool hasPaint = false;

	// Use this for initialization
	void Awake() 
	{
		if (!paint)
			paint = GetComponentInChildren<SimplePaintableObject>();
        splish = GetComponent<AudioSource>();
	}

	void Start()
	{
		if (currentColor != Color.clear)
		{
			paint.gameObject.GetComponent<Renderer>().enabled = true;
			paint.Paint(currentColor);
			hasPaint = true;
		}
        itemKey = GameManager.INSTANCE.BUCKET;
    }
	
	// Update is called once per frame
	void Update() 
	{
		
	}

	private void UsePaint(PaintableObject target)
	{
		target.Paint(currentColor);
		hasPaint = false;
		currentColor = Color.clear;
		paint.Paint(Color.clear);
		paint.gameObject.GetComponent<Renderer>().enabled = false;
	}

	private void FillBucket(Color c)
	{
		if (!hasPaint)
		{
			hasPaint = true;
			paint.gameObject.GetComponent<Renderer>().enabled = true;
		}
        currentColor = new Color(Mathf.Min(currentColor.r+c.r,1f), Mathf.Min(currentColor.g + c.g, 1f), Mathf.Min(currentColor.b + c.b, 1f));
		paint.Paint(currentColor);
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
            p.SendMessage("GurgleNoise");

			// TODO: Uncomment this once PaintFountain.Interact() is implemented.
			//p.Interact();
		}
		else if (hasPaint)
		{
			PaintableObject surface = target.GetComponent<PaintableObject>();
            splish.Play();
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
		if (hasPaint && currentColor != c)
		{
			hasPaint = false;
			paint.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}
}
