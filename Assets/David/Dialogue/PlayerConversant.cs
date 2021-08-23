using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        DialogueNode currentNode;
        bool isChoosing = false;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<string> GetChoices()
        {
            yield return "Hello";
        }

        public void Next()
        {
            DialogueNode[] childern = currentDialogue.GetAllChildren(currentNode).ToArray();
            int randomIndex = Random.Range(0, childern.Count());
            currentNode = childern[randomIndex];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}
