using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives.Task
{
    [System.Serializable]
    public class TaskStatus
    {
        [SerializeField] Quest quests;
        [SerializeField] List<string> completedObjectives;

        // This Function/Method is going to be used and referenced a lot so it can be called when
        // you need to get the quests.
        public Quest GetQuest()
        {
            return quests;
        }

        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        public bool IsComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }
    }
}


