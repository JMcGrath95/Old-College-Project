using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShaneItem 
{
    public string ID;
    public string Name;
    public string Description;

    public ShaneItem(string id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }
}
