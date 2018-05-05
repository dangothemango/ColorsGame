using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMusicManager : MonoBehaviour {

    [SerializeField]
    GameObject red;
    [SerializeField]
    GameObject green;
    [SerializeField]
    GameObject blue;

    bool redOn;
    bool greenOn;
    bool blueOn;

    [SerializeField]
    AudioClip theme_0;
    [SerializeField]
    AudioClip theme_1;
    [SerializeField]
    AudioClip theme_2;
    [SerializeField]
    AudioClip theme_3;

    AudioSource sound;

    [SerializeField]
    float sweepSpeed;

    bool inLift;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
        inLift = false;
        checkFuses();
	}
	
	// Update is called once per frame
	void Update () {
        checkFuses();

        if(inLift)
        {
            sound.pitch += sweepSpeed * Time.deltaTime;
        }
	}

    void checkFuses()
    {
        redOn = red.GetComponentInChildren<FuseBox>().getFuse();
        greenOn = green.GetComponentInChildren<FuseBox>().getFuse();
        blueOn = blue.GetComponentInChildren<FuseBox>().getFuse();

        if (blueOn)
        {
            sound.clip = theme_3;
            // Change audio properties to 3D
            sound.spatialize = true;
            sound.spatialBlend = 1.0f;
        }
        else if (greenOn)
        {
            sound.clip = theme_2;
        }
        else if (redOn)
        {
            sound.clip = theme_1;
        }
        else
        {
            sound.clip = theme_0;
        }
        if (!sound.isPlaying)
            sound.Play();
    }

    public void PlayerInLift()
    {
        inLift = true;
    }
}
