using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoryNode", menuName = "Scriptable Objects/MemoryNode")]
public class MemoryNode : ScriptableObject
{
    public enum NodeType
    {
        Memory, 
        Choice
    }

    public Action<MemoryFact> OnDropFact;
    public Action<MemoryNode> OnCompletedNode;
    
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private NodeType nodeType;
    
    [Header("Locking")]
    [SerializeField] private bool isLocked;
    
    [Header("Completion")]
    [SerializeField] private bool isCompleted;
    [SerializeField] private bool givesFactOnComplete;
    [SerializeField] private MemoryFact providedFactOnComplete;
    
    public string Id => id;
    public NodeType ThisNodeType => nodeType;
    public bool IsLocked => isLocked;
    public bool IsCompleted => isCompleted;
    public bool GivesFactOnComplete => givesFactOnComplete;
    public MemoryFact ProvidedFactOnComplete => providedFactOnComplete;

    public void UnlockNode()
    {
        isLocked = false;
    }
    
    public void CompleteNode()
    {
        if (givesFactOnComplete)
            OnDropFact?.Invoke(providedFactOnComplete);
        
        isCompleted = true;
        OnCompletedNode?.Invoke(this);
    }
}

[Serializable]
public class MemoryEdge
{
    public string Id = Guid.NewGuid().ToString();
    public MemoryNode FromNode;
    public MemoryNode ToNode;
    public MemoryFact RequiredFact;
    public string ChoiceLabel;
    public string LockedHint;
}
