using UnityEngine;

[CreateAssetMenu(fileName = "MemoryNode", menuName = "Scriptable Objects/MemoryNode")]
public class MemoryNode : ScriptableObject
{
    public enum NodeType
    {
        Memory,
        Choice
    }
    [SerializeField] private LevelData levelData;
    
    [SerializeField] private string memoryText;
    [SerializeField] private NodeType nodeType;
    
    [Header("Locking")]
    [SerializeField] private bool isLocked;
    [SerializeField] private bool needsKey;
    [SerializeField] private MemoryNodeKey keyToUnlock;
    
    [Header("Completion")]
    [SerializeField] private bool isCompleted;
    [SerializeField] private bool opensOnComplete;
    
    [Header("Branching")]
    [SerializeField] private bool branches;
    [SerializeField] private MemoryNode[] branchedMemoryNodes;
    
    [Header("Next Node")]
    [SerializeField] private bool isEndNode;
    [SerializeField] private MemoryNode nextMemoryNode;

    public LevelData LevelData => levelData;
    public string MemoryText => memoryText;
    public NodeType ThisNodeType => nodeType;
    public bool IsLocked => isLocked;
    public bool NeedsKey => needsKey;
    public MemoryNodeKey KeyToUnlock => keyToUnlock;
    public bool IsCompleted => isCompleted;
    public bool OpensOnComplete => opensOnComplete;
    public bool Branches => branches;
    public MemoryNode[] BranchedMemoryNodes => branchedMemoryNodes;
    public bool IsEndNode => isEndNode;
    public MemoryNode NextMemoryNode => nextMemoryNode;

    public void UnlockNode(MemoryNodeKey key)
    {
        if (needsKey)
        {
            if (key != keyToUnlock)
                return;
        }
        
        isLocked = false;
    }
    
    public void CompleteNode()
    {
        isCompleted = true;
    }
}
