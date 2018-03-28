using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BucketUI : MonoBehaviour
{
    RawImage b_color;
    [SerializeField] Bucket bucket;

    void Start()
    {
        b_color = GetComponent<RawImage>();
        b_color.enabled = true;
    }

    void Update()
    {
        b_color.color = bucket.currentColor;
    }
}