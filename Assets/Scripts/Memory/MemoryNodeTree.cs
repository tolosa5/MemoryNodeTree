using System.Collections.Generic;
using UnityEngine;

public class MemoryRun 
{
    public string currentNodeId;
    public List<MemoryFact> pendingFacts = new();
}

public class MemoryNodeTree : MonoBehaviour
{
    public string StartNodeId;
    [SerializeField] List<MemoryNode> nodes = new();
    [SerializeField] List<MemoryEdge> edges = new();
    
    Dictionary<string, MemoryNode> nodeById;
    Dictionary<string, List<MemoryEdge>> outgoing, incoming;
    Dictionary<string, List<MemoryEdge>> edgesByFact;
    private List<MemoryFact> confirmedFacts = new();

    public List<MemoryEdge> GetOutgoing(string nodeId) => outgoing[nodeId];
    public List<MemoryEdge> GetIncoming(string nodeId) => incoming[nodeId];
    public bool IsBranching(string nodeId) => outgoing[nodeId].Count > 1;
    public bool IsEnding(string nodeId) => outgoing[nodeId].Count == 0;
}
