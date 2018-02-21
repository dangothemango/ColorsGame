using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public RawImage uiimage;
    public Text txt;

    void Start()
    {
        uiimage = GetComponent<RawImage>();
        uiimage.enabled = false;
        txt.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && uiimage.name == "mapui")
        {
            uiimage.enabled = !uiimage.enabled;
        }
        else if (Input.GetKeyUp(KeyCode.I) && uiimage.name == "bucketinv")
        {
            uiimage.enabled = !uiimage.enabled;
        }
        else if (Input.GetKeyUp(KeyCode.C) && uiimage.name == "bucketcol")
        {
            uiimage.enabled = !uiimage.enabled;
        }
    }
}