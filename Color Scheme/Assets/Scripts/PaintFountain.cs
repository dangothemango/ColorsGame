using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFountain : InteractableObject {

	[Header("Paint Fountain Parameters")]
	public Color col;   // Change this to SerializeField, please
	[SerializeField] private SimplePaintableObject paintPool;
    private AudioSource gurgle;

    private void Awake() {
        DoAwake();
    }

    // Use this for initialization
    void Start () {
        DoStart();
	}

	protected override void DoStart()
	{
		base.DoStart();
		if (!paintPool)
			paintPool = GetComponentInChildren<SimplePaintableObject>();
		paintPool.Paint(col);
		paintPool.enabled = false;
        gurgle = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        DoUpdate();
	}

	// TODO: Do some animation of paint fountain filling paint bucket
    public override void Interact() {
        base.Interact();
    }

    public void GurgleNoise()
    {
        gurgle.Play();
    }
}
