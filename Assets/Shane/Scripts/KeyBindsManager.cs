using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    //Keybind was changed event.
    public static event Action<string,KeyCode> KeyBindChangedEvent;

    //Keycodes available.
    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>().ToArray();

    //Default keybinds in game. Set in inspector.
    [SerializeField] private List<KeyBind> defaultKeybindsList = new List<KeyBind>();

    //Keybinds in game.
    public static Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        foreach (var keyBind in defaultKeybindsList)
        {
            keyBinds.Add(keyBind.actionName, keyBind.keyCode);
        }
    }

    private void Start()
    {
        //ChangeKeybind("Attack", KeyCode.K);
    }

    public static void ChangeKeybind(string actionName,KeyCode keyCodeToAssign)
    {
        //Does this action exist in game?
        if (!keyBinds.ContainsKey(actionName))
        {
            Debug.LogWarning($"Can't change keybind. No action exists for {actionName}.");
            return;
        }

        //Keybinds already has that key in use?
        if (KeyAlreadyInUse(keyCodeToAssign))
        {
            Debug.LogWarning($"Can't Change Keybind. Key {keyCodeToAssign} is already in use by the action {keyBinds.FirstOrDefault(kb => kb.Value == keyCodeToAssign).Key}.");
            return;
        }


        //Change keybind.
        KeyBind keyBindBeingChanged = new KeyBind() { actionName = actionName, keyCode = keyBinds[actionName] };

        keyBinds[actionName] = keyCodeToAssign;
        KeyBindChangedEvent?.Invoke(actionName,keyCodeToAssign);

        KeyBind newKeyBind = new KeyBind() { actionName = actionName, keyCode = keyCodeToAssign };

        print($"Keybind - {keyBindBeingChanged} changed to {newKeyBind}");


        //Save to JSON?

    }

    //Is a key already used in a keybind?
    public static bool KeyAlreadyInUse(KeyCode keyCodeToCheck) => keyBinds.ContainsValue(keyCodeToCheck);
}


[Serializable]
public class KeyBind
{
    public string actionName;
    public KeyCode keyCode;

    public override string ToString()
    {
        return $"{actionName} : {keyCode}";
    }
}
