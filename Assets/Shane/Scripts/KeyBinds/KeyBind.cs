using System;
using UnityEngine;

[Serializable]
public class KeyBind
{
    public string actionName;
    public KeyCode keyCode;

    public KeyBind() { }
    public KeyBind(string actionName, KeyCode keyCode)
    {
        this.actionName = actionName;
        this.keyCode = keyCode;
    }

    public override string ToString()
    {
        return $"{actionName} : {keyCode}";
    }
}

