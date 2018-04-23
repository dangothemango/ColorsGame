using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : PlayerItem 
{
    private static Func<Color, Color, Color> addColors = delegate (Color a, Color b) {
        return new Color(Mathf.Min(a.r + b.r, 1f), Mathf.Min(a.g + b.g, 1f), Mathf.Min(a.b + b.b, 1f));
    };

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
		EmptyBucket();
	}

    private void FillBucket(Color c) {
        if (!hasPaint) {
            hasPaint = true;
            paint.gameObject.GetComponent<Renderer>().enabled = true;
        }
        currentColor = addColors(currentColor,c);
        paint.Paint(currentColor);
    }

	public override bool CanUseOn(InteractableObject target)
	{
        if (target == null) { return false; }
		return (target is PaintFountain) || (hasPaint && target.GetComponent<PaintableObject>());
	}

	public override void UseOn(InteractableObject target)
	{
        if (target == null) { return; }
		if (target is PaintFountain)
		{
			PaintFountain p 	= target as PaintFountain;
			FillBucket(p.color);
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
			EmptyBucket();
		}
	}

	public override void SecondaryUsage()
	{
		if (hasPaint)
		{
			EmptyBucket();
		}
	}

	private void EmptyBucket()
	{
		hasPaint = false;
		currentColor = Color.clear;
		paint.Paint(currentColor);
		paint.gameObject.GetComponent<Renderer>().enabled = false;
	}

    public override Sprite GetTooltipIcon(InteractableObject io, out Color c) {
        if (io is PaintFountain) {
            c = addColors((io as PaintFountain).color,currentColor);
            return secondaryTooltip;
        }
        c = currentColor;
        return primaryTooltip;
    }
}
