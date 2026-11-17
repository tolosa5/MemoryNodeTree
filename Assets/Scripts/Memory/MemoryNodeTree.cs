using System;
using System.Collections.Generic;
using UnityEngine;

public class MemoryNodeTree : MonoBehaviour
{
    private MemoryNode currentNode;
    private MemoryNode[] memoryNodes;
    private List<MemoryNode> unlockedMemoryNodes = new List<MemoryNode>();
    
    private List<MemoryNodeKey> collectedKeys = new List<MemoryNodeKey>();

    private void Start()
    {
        foreach (MemoryNode memoryNode in memoryNodes)
        {
            memoryNode.OnDropKey += CollectKey;
            memoryNode.OnCompletedNode += NodeCompleted;
        }
    }
    
    private void CollectKey(MemoryNodeKey key)
    {
        if (!collectedKeys.Contains(key))
            collectedKeys.Add(key);
    }
    
    private void NodeCompleted(MemoryNode node)
    {
        unlockedMemoryNodes.Add(node);
        if (node.IsEndNode)
            return;
        
        currentNode = node.NextMemoryNode;
    }
}
