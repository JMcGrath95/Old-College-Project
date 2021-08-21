using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{

    [System.Serializable]
    public class DialogueNode
    {
        public string DialogueUniqueID;
        public string text;
        public string[] childern;
    }

}