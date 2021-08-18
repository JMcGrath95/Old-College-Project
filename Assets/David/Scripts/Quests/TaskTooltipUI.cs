using DaveCore.UI.Objectives.Task;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DaveCore.UI.Objectives
{

    public class TaskTooltipUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveInCompletePrefab;

        public void Setup(TaskStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            objectiveContainer.DetachChildren();

            foreach (string objective in quest.GetObjectives())
            {
                GameObject prefab = objectiveInCompletePrefab;
                if(status.IsComplete(objective))
                {
                    prefab = objectivePrefab;
                }

                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective;
            }
        }
    }
}
