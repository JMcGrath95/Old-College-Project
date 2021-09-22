using GameDevTV.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives.Task
{
    [CreateAssetMenu(fileName = "Task", menuName = "RPG/Project/Task", order = 0)]

    public class Quest : ScriptableObject {
        //[SerializeField] string[] objectives;
        [SerializeField] List<Objective> objectives = new List<Objective>();

        [System.Serializable]
        public class Objective
        {
            public string reference;
            public string description;
            public bool usesCondition = false;
            public Condition completionCondition;
        }

        public string GetTitle()
        {
            return name;
        }

        //This simply returning the number of objectives
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }
        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if (objective.reference == objectiveRef)
                {
                    return true;
                }
            }
            return false;
        }
    }
}


