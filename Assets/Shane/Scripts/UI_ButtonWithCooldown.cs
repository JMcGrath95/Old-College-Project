using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonWithCooldown : MonoBehaviour
{
    protected UI_ProgressBar progressBar;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        progressBar = GetComponentInParent<UI_ProgressBar>();
    }
    protected virtual void Start() { }

    protected void DisableButton() => button.interactable = false;
    protected void EnableButton() => button.interactable = true;

    protected void EnableButtonCooldown(float cooldownLength)
    {
        Timer cooldownTimer = new Timer(this,cooldownLength, EnableButton);
    }
}
