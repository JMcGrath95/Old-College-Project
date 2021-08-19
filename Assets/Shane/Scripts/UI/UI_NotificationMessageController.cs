using UnityEngine;

public class UI_NotificationMessageController : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private UI_NotificationMessage notificationMessagePrefab;

    [Header("Moving Targets For Messages")]
    [SerializeField] private Transform spawnPosForMessage;
    [SerializeField] private Transform targetPosForMessage;

    public void ShowMessage(string message)
    {
        UI_NotificationMessage notificationMessage = Instantiate(notificationMessagePrefab, spawnPosForMessage.position, Quaternion.identity,this.transform);
        notificationMessage.SetMessage(message);
        notificationMessage.MoveToTargetAndFade(targetPosForMessage.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowMessage("This is a message");
        }
    }
}
