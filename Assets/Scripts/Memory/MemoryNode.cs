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

    public Action<MemoryFact> OnDropKey;
    public Action<MemoryNode> OnCompletedNode;
    
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private NodeType nodeType;
    
    [Header("Locking")]
    [SerializeField] private bool isLocked;
    [SerializeField] private bool needsKey;
    [SerializeField] private MemoryFact factToUnlock;
    
    [Header("Completion")]
    [SerializeField] private bool isCompleted;
    [SerializeField] private bool opensOnComplete;
    [SerializeField] private MemoryFact providedFactOnComplete;
    
    public string Id => id;
    public NodeType ThisNodeType => nodeType;
    public bool IsLocked => isLocked;
    public bool NeedsKey => needsKey;
    public MemoryFact FactToUnlock => factToUnlock;
    public bool IsCompleted => isCompleted;
    public bool OpensOnComplete => opensOnComplete;
    public MemoryFact ProvidedFactOnComplete => providedFactOnComplete;

    public void UnlockNode(MemoryFact key)
    {
        if (needsKey)
        {
            if (key != factToUnlock)
                return;
        }
        
        isLocked = false;
    }
    
    public void CompleteNode()
    {
        if (opensOnComplete)
            OnDropKey?.Invoke(providedFactOnComplete);
        
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
    public string ChoiceLabel;
    public string LockedHint;
}
