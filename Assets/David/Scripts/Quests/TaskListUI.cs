using DaveCore.UI.Objectives.Task;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListUI : MonoBehaviour
{
    //[SerializeField] Quest[] tempTasks;
    [SerializeField] TaskItemUITool TaskPrefab;

    void Start()
    {
        //This is to clear quest List that would clear any of the existing children
        //So any test UI that was being used as a placeholder would be reset.

        transform.DetachChildren();
        QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();

        foreach (TaskStatus status in questList.GetStatus())
        {
            TaskItemUITool uiInstance = Instantiate<TaskItemUITool>(TaskPrefab, transform);
            uiInstance.Setup(status);
        }
    }

    
}
