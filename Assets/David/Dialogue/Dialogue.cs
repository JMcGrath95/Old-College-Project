using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{

    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]

    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeSearcher = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count() == 0)
            {
                DialogueNode rootNode = new DialogueNode();
                rootNode.DialogueUniqueID = System.Guid.NewGuid().ToString();
                nodes.Add(rootNode);
            }

            OnValidate();
        }
#endif
        private void OnValidate()
        {
            nodeSearcher.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeSearcher[node.DialogueUniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            List<DialogueNode> result = new List<DialogueNode>();
            foreach (string ChildID in parentNode.childern)
            {
                if(nodeSearcher.ContainsKey(ChildID))
                {
                    result.Add(nodeSearcher[ChildID]);
                }
            }
            return result;
        }

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.DialogueUniqueID = Guid.NewGuid().ToString();
            parent.childern.Add(newNode.DialogueUniqueID);
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.childern.Remove(nodeToDelete.DialogueUniqueID);
            }
        }
    }
}

