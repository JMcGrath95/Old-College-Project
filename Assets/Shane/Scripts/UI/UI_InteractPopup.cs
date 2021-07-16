using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InteractPopup : MonoBehaviour
{
    [Header("Components")]
    private TextMeshProUGUI txtInteractMessage;

    private void Awake()
    {
        txtInteractMessage = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        txtInteractMessage.text = $"{KeyBindsManager.keyBinds["Interact"]} : Interact";
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
