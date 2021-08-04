using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    public static KeyBindsManager Instance;

    //Keycodes available.
    public static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>().ToArray();


    [Header("Testing")]
    [Tooltip("Set keybinds as default instead of reading from json file settings. ")]
    [SerializeField] private bool SetDefaultKeybinds;

    //Default keybinds in game. Set in inspector.
    [SerializeField] private List<KeyBind> defaultKeybindsList = new List<KeyBind>();

    //Keybinds in game which InputManager reads from.
    public static Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();

    //JSON Saving Keybind Settings To JSON File.
    public static readonly string keybindsJSONFolderPath = "/Shane/Keybinds.txt";
    public static string keybindsJSONFullPath;
    KeyBind[] keyBindsArray;

    private void Awake()
    {
        keybindsJSONFullPath = Application.dataPath + keybindsJSONFolderPath;

        //Set Up Singleton.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        if(SetDefaultKeybinds)
        {
            keyBindsArray = defaultKeybindsList.ToArray();
        }
        else //Check if keybinds settings JSON exists.
        {
            if (File.Exists(keybindsJSONFullPath)) //Keybinds settings file exists. Read from it and set keybinds to this.
            {
                //Attempt to convert from JSON.
                try
                {
                    keyBindsArray = JSONHelper.FromJson<KeyBind>(File.ReadAllText(keybindsJSONFullPath));
                }
                catch (Exception) //Json file not correct, revert to default keybind settings.
                {
                    Debug.LogError("Could not convert Keybinds JSON Text File into keybinds array.");

                    SaveDefaultKeybindsToJSON();
                }
            }
            else //No keybinds settings JSON - create one and set settings to deafult.
            {
                SaveDefaultKeybindsToJSON();
            }
        }      

        //Add to Dictionary.
        foreach (var keyBind in keyBindsArray)
        {
            keyBinds.Add(keyBind.actionName, keyBind.keyCode);
        }
    }

    #region Saving KeyBinds To JSON File
    private void SaveDefaultKeybindsToJSON()
    {
        keyBindsArray = defaultKeybindsList.ToArray();

        string deafultKeybindsJSON = JSONHelper.ToJson(keyBindsArray, true);
        File.WriteAllText(keybindsJSONFullPath, deafultKeybindsJSON);
    }
    public static void SaveKeybindsToJSON()
    {
        //Saves dictionary to text file.
        KeyBind[] keyBindsToSave = new KeyBind[keyBinds.Count];
        for (int i = 0; i < keyBinds.Count; i++)
        {
            keyBindsToSave[i] = new KeyBind(keyBinds.Keys.ElementAt(i), keyBinds.Values.ElementAt(i));
        }

        string keyBindsJSON = JSONHelper.ToJson(keyBindsToSave);

        try
        {
            File.WriteAllText(keybindsJSONFullPath, keyBindsJSON);
            print("Keybind settings saved to JSON File.");
        }
        catch (Exception)
        {
            Debug.LogError("Could not save keybinds to JSON file.");
        }
    }
    #endregion

    #region Change Keybind - Alters Keybind Dictionary
    public static bool AttemptToChangeKeybind(KeyBind keyBind)
    {
        //Does this action exist in game?
        if (!keyBinds.ContainsKey(keyBind.actionName))
        {
            Debug.LogWarning($"Can't change keybind. No action exists for {keyBind.actionName}.");
            return false;
        }

        //Keybinds already has that key in use?
        if (KeyAlreadyInUse(keyBind.keyCode))
        {
            Debug.LogWarning($"Can't Change Keybind. Key {keyBind.keyCode} is already in use by the action {keyBinds.FirstOrDefault(kb => kb.Value == keyBind.keyCode).Key}.");
            return false;
        }

        //Change keybind.
        KeyBind keyBindBeingChanged = new KeyBind() { actionName = keyBind.actionName, keyCode = keyBinds[keyBind.actionName] };

        keyBinds[keyBind.actionName] = keyBind.keyCode;

        KeyBind newKeyBind = new KeyBind() { actionName = keyBind.actionName, keyCode = keyBind.keyCode };

        print($"Keybind changed! {keyBindBeingChanged} changed to {newKeyBind}");

        return true;
    }

    //Is a key already used in a keybind?
    public static bool KeyAlreadyInUse(KeyCode keyCodeToCheck) => keyBinds.ContainsValue(keyCodeToCheck);

    #endregion


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
