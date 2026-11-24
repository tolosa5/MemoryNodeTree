using System.Collections.Generic;
using UnityEngine;

public class MemoryNodeTree : MonoBehaviour
{
    public static MemoryNodeTree Instance { get; private set; }
    
    public string StartNodeId;
    [SerializeField] List<MemoryNode> nodes = new();
    [SerializeField] List<MemoryEdge> edges = new();
    
    Dictionary<string, MemoryNode> nodeById;
    Dictionary<string, List<MemoryEdge>> outgoing, incoming;
    Dictionary<string, List<MemoryEdge>> edgesByFact;
    private List<MemoryFact> confirmedFacts = new();

    public List<MemoryEdge> GetOutgoing(string nodeId) => outgoing[nodeId];
    public List<MemoryEdge> GetIncoming(string nodeId) => incoming[nodeId];
    public List<MemoryFact> GetConfirmedFacts() => confirmedFacts;
    public bool IsBranching(string nodeId) => outgoing[nodeId].Count > 1;
    public bool IsEnding(string nodeId) => outgoing[nodeId].Count == 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        BuildEdges();
    }

    private void BuildEdges()
    {
        nodeById = new Dictionary<string, MemoryNode>();
        outgoing = new Dictionary<string, List<MemoryEdge>>();
        incoming = new Dictionary<string, List<MemoryEdge>>();
        edgesByFact = new Dictionary<string, List<MemoryEdge>>();

        foreach (var node in nodes)
        {
            nodeById[node.Id] = node;
        }

        foreach (var edge in edges)
        {
            //esto aun no contempla el caso de que un nodo tenga multiples edges salientes o entrantes
            if (!outgoing.ContainsKey(edge.FromNode.Id))
            {
                outgoing[edge.FromNode.Id] = new List<MemoryEdge>();
                outgoing[edge.FromNode.Id].Add(edge);
            }

            if (!incoming.ContainsKey(edge.FromNode.Id))
            {
                incoming[edge.FromNode.Id] = new List<MemoryEdge>();
                incoming[edge.FromNode.Id].Add(edge);
            }

            
            if (edge.RequiredFact != null)
            {
                if (!edgesByFact.ContainsKey(edge.RequiredFact.Id))
                {
                    edgesByFact[edge.RequiredFact.Id] = new List<MemoryEdge>();
                    edgesByFact[edge.RequiredFact.Id].Add(edge);
                }
            }
        }
    }
}
