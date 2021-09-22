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

        [System.Serializable]
        class QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;
        }

        public TaskStatus(Quest quest)
        {
            this.quests = quest;
        }

        public TaskStatus(object objectState)
        {
            QuestStatusRecord state = objectState as QuestStatusRecord;
            quests = Quest.GetByName(state.questName);
            completedObjectives = state.completedObjectives;
        }

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

        public bool IsComplete()
        {
            foreach (var objective in quests.GetObjectives())
            {
                if (!completedObjectives.Contains(objective.reference))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
        {
            if (quests.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
        }
    }
}


