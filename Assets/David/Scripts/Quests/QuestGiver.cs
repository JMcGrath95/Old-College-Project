using DaveCore.UI.Objectives.Task;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaveCore.UI.Objectives
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;

        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.AddQuest(quest);
        }
    }
}
