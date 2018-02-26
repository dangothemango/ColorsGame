using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BucketUI : MonoBehaviour
{
    RawImage b_color;
    private bool have_bucket;

    void Start()
    {
        b_color = GetComponent<RawImage>();
        b_color.enabled = false;
        have_bucket = false;
    }

    void Update()
    {
        Bucket b = (Bucket)FindObjectOfType(typeof(Bucket));
        if (!have_bucket)
        {
            if (b)
            {
                b_color.enabled = true;
                have_bucket = true;
            }
        }
        else
        {
            b_color.color = b.currentColor;
        }
    }
}