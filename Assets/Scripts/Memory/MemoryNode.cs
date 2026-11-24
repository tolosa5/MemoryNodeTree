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
    //si un nodo esta bloqueado, no se puede acceder a el ni ver el iconito hasta que se desbloquee
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
    
    //aun por decidir desde donde se llama a esta funcion, quiza que escuche evento desde run
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
    //si esta bloqueado porque necesites conocer algo de otra rama temporal
    public MemoryFact RequiredFact;
    public RunDecisions BranchDecision;
    public string ChoiceLabel;
    public string LockedHint;
}
