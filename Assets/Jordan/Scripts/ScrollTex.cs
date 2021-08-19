using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTex : MonoBehaviour
{
    public float scrlX, scrlY;
    void Update()
    {
        float OffX = Time.time * scrlX;
        float OffY = Time.time * scrlY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffX, OffY);
    }
}
