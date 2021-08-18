using DaveCore.UI.Objectives.Task;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskItemUITool : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI progress;

    TaskStatus status;

    public void Setup(TaskStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();
        progress.text = status.GetCompletedCount() + "/" + status.GetQuest().GetObjectiveCount();
    }

   public TaskStatus GetQuestStatus()
    {
        return status;
    }
}
