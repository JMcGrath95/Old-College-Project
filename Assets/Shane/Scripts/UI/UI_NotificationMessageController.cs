using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack speed, damage, health - addition.

public class UI_NotificationMessageController : MonoBehaviour
{
    public static UI_NotificationMessageController Instance;

    [Header("Prefab")]
    [SerializeField] private UI_NotificationMessage notificationMessagePrefab;

    [Header("Moving Targets For Messages")]
    [SerializeField] private Transform spawnPosForMessage;
    [SerializeField] private Transform targetPosForMessage;

    [Header("Delay between messages appearing")]
    [SerializeField] private float messageSpawnDelay;

    private Queue<string> messageQueue = new Queue<string>();
    private Coroutine processMessageCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowMessage(string message)
    {
        messageQueue.Enqueue(message);

        if(processMessageCoroutine == null)
        {
            processMessageCoroutine = StartCoroutine(ProcessMessagesCoroutine());
        }
    }

    private IEnumerator ProcessMessagesCoroutine()
    {
        while(messageQueue.Count > 0)
        {
            SpawnMessage(messageQueue.Dequeue()); //Spawn and remove from queue.

            yield return new WaitForSeconds(messageSpawnDelay);
        }

        processMessageCoroutine = null;
    }

    private void SpawnMessage(string message)
    {
        UI_NotificationMessage notificationMessage = Instantiate(notificationMessagePrefab, spawnPosForMessage.position, Quaternion.identity, this.transform);
        notificationMessage.SetMessage(message);
        notificationMessage.MoveToTargetAndFade(targetPosForMessage.position);
    }

    //Testing.
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowMessage("Message");
        }
    }
}