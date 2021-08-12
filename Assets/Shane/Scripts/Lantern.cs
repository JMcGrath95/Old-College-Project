using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = defaultRotation; 
    }
}
