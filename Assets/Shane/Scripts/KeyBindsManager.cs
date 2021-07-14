using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    //Keybind was changed event.
    public static event Action<KeyBind> KeyBindChangedEvent;

    //Keycodes available.
    public static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
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
        DontDestroyOnLoad(this);
    }

    public static bool AttemptToChangeKeybind(KeyBind keyBind/*, Action<bool,KeyBind> callback*/)
    {
        //Does this action exist in game?
        if (!keyBinds.ContainsKey(keyBind.actionName))
        {
            Debug.LogWarning($"Can't change keybind. No action exists for {keyBind.actionName}.");
            //callback(false,null);
            return false;
        }

        //Keybinds already has that key in use?
        if (KeyAlreadyInUse(keyBind.keyCode))
        {
            Debug.LogWarning($"Can't Change Keybind. Key {keyBind.keyCode} is already in use by the action {keyBinds.FirstOrDefault(kb => kb.Value == keyBind.keyCode).Key}.");
            //callback(false,null);
            return false;
        }

        //Change keybind.

        KeyBind keyBindBeingChanged = new KeyBind() { actionName = keyBind.actionName, keyCode = keyBinds[keyBind.actionName] };

        keyBinds[keyBind.actionName] = keyBind.keyCode;
        KeyBindChangedEvent?.Invoke(keyBind);

        KeyBind newKeyBind = new KeyBind() { actionName = keyBind.actionName, keyCode = keyBind.keyCode };
        //callback(true,newKeyBind);

        print($"Keybind changed! {keyBindBeingChanged} changed to {newKeyBind}");

        return true;

        //Save to JSON?
    }

    //Is a key already used in a keybind?
    public static bool KeyAlreadyInUse(KeyCode keyCodeToCheck) => keyBinds.ContainsValue(keyCodeToCheck);

    public static KeyCode GetCurrentKeyDown()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                return keyCodes[i];
            }
        }
        return KeyCode.None;
    }
}


[Serializable]
public class KeyBind
{
    public string actionName;
    public KeyCode keyCode;

    public KeyBind() { }
    public KeyBind(string actionName,KeyCode keyCode)
    {
        this.actionName = actionName;
        this.keyCode = keyCode;
    }

    public override string ToString()
    {
        return $"{actionName} : {keyCode}";
    }
}
