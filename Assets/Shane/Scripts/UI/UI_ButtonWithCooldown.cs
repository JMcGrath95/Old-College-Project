using UnityEngine;
using UnityEngine.UI;

//A button which can be disabled and enabled after a certain time.

public class UI_ButtonWithCooldown : MonoBehaviour
{
    protected Button button;

    protected virtual void Awake() => button = GetComponent<Button>();

    protected void DisableButton() => button.interactable = false;
    protected void EnableButton() => button.interactable = true;

    protected void EnableButtonCooldown(float cooldownLength)
    {
        Timer timer = new Timer(this,cooldownLength, EnableButton);
    }
}