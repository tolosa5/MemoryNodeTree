using System;
using System.Collections.Generic;

//Las diferentes decisiones que puedes tomar
public enum RunDecisions
{
    A,
    B,
    C
};

public class MemoryRun 
{
    public string currentNodeId;
    //las cosas que aprendes no se confirman hasta que completas el nodo, por lo que se guardan en pendingFacts
    public List<MemoryFact> pendingFacts = new();
    //la ultima decision que tomaste, se usa para determinar a que nodo ir en caso de que haya una bifurcacion
    public RunDecisions runDecision;
    
    public Action<string> OnCompletedNode;
    public Action<MemoryRun> OnCompletedRun;
    
    public void AddFact(MemoryFact fact)
    {
        pendingFacts.Add(fact);
    }

    public void NodeCompleted()
    {
        //aun por decidir si esta funcion la llama un evento desde el nodo
        //o si la llama el run y el evento llegar a memoryNodeTree, quiza ambos rollo cadena?
        OnCompletedNode?.Invoke(currentNodeId);
        ProgressToNode(GetNextNodeId());
    }
    
    private string GetNextNodeId()
    {
        foreach (var edge in MemoryNodeTree.Instance.GetOutgoing(currentNodeId))
        {
            //si es un caso de avance lineal y ya, pues va al siguiente
            if (edge.RequiredFact == null || MemoryNodeTree.Instance.GetConfirmedFacts().Contains(edge.RequiredFact) && 
                !MemoryNodeTree.Instance.IsBranching(currentNodeId))
            {
                return edge.ToNode.Id;
            }
            //si es bifurcacion, busca de los nodos posibles el que tenga la decision que tomaste
            if (edge.RequiredFact == null || MemoryNodeTree.Instance.GetConfirmedFacts().Contains(edge.RequiredFact) && 
                     MemoryNodeTree.Instance.IsBranching(currentNodeId))
            {
                MemoryEdge branchingEdge = MemoryNodeTree.Instance.GetOutgoing(currentNodeId).
                    Find(e => e.BranchDecision == runDecision);
                
                if (branchingEdge != null)
                    return branchingEdge.ToNode.Id;
            }
        }

        return null;
    }
    
    private void ProgressToNode(string nodeId)
    {
        currentNodeId = nodeId;
    }

    public void CompleteRun()
    {
        OnCompletedRun?.Invoke(this);
    }
}