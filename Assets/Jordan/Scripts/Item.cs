using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int value;
    public int Range;
    private void Start()
    {
        value = Random.Range(0, Range); 
    }

    
   
}
