using GameDevTV.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives.Task
{
    
    public class QuestList : MonoBehaviour, IPredicateEvaluator
    {
        List<TaskStatus> statuses = new List<TaskStatus>();

        public event Action onUpdate;

        private void Update()
        {
            CompleteObjectivesByPredicates();
        }

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            TaskStatus newStatus = new TaskStatus(quest);
            statuses.Add(newStatus);
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            TaskStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (status.IsComplete())
            {
                //GiveReward(quest);
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<TaskStatus> GetStatuses()
        {
            return statuses;
        }

        private TaskStatus GetQuestStatus(Quest quest)
        {
            foreach (TaskStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        //private void GiveReward(Quest quest)
        //{
        //    foreach (var reward in quest.GetRewards())
        //    {
        //        bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
        //        if (!success)
        //        {
        //            GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
        //        }
        //    }
        //}

        private void CompleteObjectivesByPredicates()
        {
            foreach (TaskStatus status in statuses)
            {
                if (status.IsComplete()) continue;
                Quest quest = status.GetQuest();
                foreach (var objective in quest.GetObjectives())
                {
                    if (status.IsObjectiveComplete(objective.reference)) continue;
                    if (!objective.usesCondition) continue;
                    if (objective.completionCondition.Check(GetComponents<IPredicateEvaluator>()))
                    {
                        CompleteObjective(quest, objective.reference);
                    }
                }
            }
        }

        //public object CaptureState()
        //{
        //    List<object> state = new List<object>();
        //    foreach (TaskStatus status in statuses)
        //    {
        //        state.Add(status.CaptureState());
        //    }
        //    return state;
        //}

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            statuses.Clear();
            foreach (object objectState in stateList)
            {
                statuses.Add(new TaskStatus(objectState));
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
            }

            return null;
        }
    }
}
