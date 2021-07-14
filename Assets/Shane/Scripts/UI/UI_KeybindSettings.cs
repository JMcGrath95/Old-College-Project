using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI_KeybindSettings : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Button keybindActionPrefab;
    [SerializeField] private UI_Keybind keybindKeyPrefab;


    private void Start()
    {
        for (int i = 0; i < KeyBindsManager.keyBinds.Count; i++)
        {
            Button btnKeyBindAction = Instantiate(keybindActionPrefab, transform);
            btnKeyBindAction.GetComponentInChildren<TextMeshProUGUI>().text = KeyBindsManager.keyBinds.Keys.ElementAt(i);

            UI_Keybind ui_Keybind = Instantiate(keybindKeyPrefab, transform);
            ui_Keybind.SetKeybind(new KeyBind(KeyBindsManager.keyBinds.Keys.ElementAt(i).ToString(), KeyBindsManager.keyBinds.Values.ElementAt(i)));
        }
    }
}