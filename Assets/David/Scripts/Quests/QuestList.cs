using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives.Task
{
    
    public class QuestList : MonoBehaviour
    {
        [SerializeField] TaskStatus[] statuses;

        public IEnumerable<TaskStatus> GetStatus()
        {
            return statuses;
        }
    }
}
