using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public bool uiimage;
    public Image b;
    public Image f;
    public Image e;
    public Image fus;

    void Start()
    {
        uiimage = false;
        b.enabled = false;
        f.enabled = false;
        e.enabled = false;
        fus.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            uiimage = !uiimage;
            b.enabled = false;
            f.enabled = false;
            e.enabled = false;
            fus.enabled = false;
        }
        if (uiimage)
        {
            if (Player.INSTANCE.bucket())
            {
                b.enabled = true;
            }
            if (Player.INSTANCE.flashlight())
            {
                f.enabled = true;
            }
            if (Player.INSTANCE.eyedropper())
            {
                e.enabled = true;
            }
            if (Player.INSTANCE.fuse())
            {
                fus.enabled = true;
            }
        }
    }
}