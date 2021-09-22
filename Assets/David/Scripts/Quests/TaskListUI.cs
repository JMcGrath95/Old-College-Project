using DaveCore.UI.Objectives.Task;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListUI : MonoBehaviour
{
    //[SerializeField] Quest[] tempTasks;
    [SerializeField] TaskItemUITool TaskPrefab;
    QuestList questList;

    void Start()
    {
        //This is to clear quest List that would clear any of the existing children
        //So any test UI that was being used as a placeholder would be reset.

        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        foreach (TaskStatus status in questList.GetStatuses())
        {
            TaskItemUITool uiInstance = Instantiate<TaskItemUITool>(TaskPrefab, transform);
            uiInstance.Setup(status);
        }
    }
}
