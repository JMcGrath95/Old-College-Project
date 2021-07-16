using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Button which you click on to change keybind.
public class UI_Keybind : MonoBehaviour
{
    //Components.
    private TextMeshProUGUI myText;
    private Button myButton;
    private Image myImage;


    private Color defaultColour;
    private KeyBind myKeyBind;

    private bool CheckingInputForKeybind = false;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        defaultColour = myImage.color;
        myButton.onClick.AddListener(EnableCheckingForKeybindChange);
    }

    private void Update()
    {
        if (!CheckingInputForKeybind)
            return;

        if(Input.anyKeyDown)
        {
            KeyCode keyCodePressed = KeyBindsManager.GetCurrentKeyDown();

            KeyBind myNewKeyBind = new KeyBind(myKeyBind.actionName, keyCodePressed);

            if(KeyBindsManager.AttemptToChangeKeybind(myNewKeyBind))
            {
                myKeyBind = myNewKeyBind;
            }

            DisableCheckingForKeybindChange();
        }
    }

    public void SetKeybind(KeyBind keyBind)
    {
        myKeyBind = keyBind;

        myText.text = keyBind.keyCode.ToString();
    }

    private void EnableCheckingForKeybindChange()
    {
        CheckingInputForKeybind = true;

        myText.text = "Select key..";
        myImage.color = Color.green;
    }

    private void DisableCheckingForKeybindChange()
    {
        myImage.color = defaultColour;
        myText.text = myKeyBind.keyCode.ToString();

        CheckingInputForKeybind = false;
    }
}
