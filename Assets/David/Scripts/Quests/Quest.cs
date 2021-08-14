using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives.Task
{
    [CreateAssetMenu(fileName = "Task", menuName = "RPG/Project/Task", order = 0)]

    public class Quest : ScriptableObject {
        [SerializeField] string[] objectives;

        public string GetTitle()
        {
            return name;
        }

        //This simply returning the number of objectives
        public int GetObjectiveCount()
        {
            return objectives.Length;
        }
    }
}


