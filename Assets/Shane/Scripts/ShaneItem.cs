using System;

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
