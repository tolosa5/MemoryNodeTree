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
    private GameEventBus eventBus;
    private LevelHandler levelHandler;
    //las cosas que aprendes no se confirman hasta que completas el nodo, por lo que se guardan en pendingFacts
    public List<MemoryFact> pendingFacts = new();
    //la ultima decision que tomaste, se usa para determinar a que nodo ir en caso de que haya una bifurcacion
    public RunDecisions runDecision;
    
    public MemoryRun(string startNodeId, GameEventBus eventBus)
    {
        currentNodeId = startNodeId;
        this.eventBus = eventBus;
        
        levelHandler = new LevelHandler(startNodeId, eventBus);
        eventBus.Raise(new OnLoadLevel(currentNodeId));
        
        this.eventBus.Subscribe<OnCompletedNode>(NodeCompleted);
        this.eventBus.Subscribe<OnDropMemoryFact>(AddFact);
    }
    
    private void AddFact(OnDropMemoryFact ev)
    {
        pendingFacts.Add(ev.memoryFact);
    }

    private void NodeCompleted(OnCompletedNode ev)
    {
        //aun por decidir si esta funcion la llama un evento desde el nodo
        //o si la llama el run y el evento llegar a memoryNodeTree, quiza ambos rollo cadena?
        
        if (MemoryNodeTree.Instance.IsEnding(currentNodeId))
        {
            CompleteRun();
            return;
        }
        
        ProgressToNode(GetNextNodeId());
        eventBus.Raise(new OnLoadLevel(currentNodeId));
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
        //Se llama al EnterNode del nodo que esta activo para sus suscripciones
        MemoryNodeTree.Instance.GetNodeById(currentNodeId).EnterNode(eventBus);
    }

    private void CompleteRun()
    {
        eventBus.Raise(new OnCompletedRun(this));
        //Deberia aqui llamarse al clear del eventbus?
    }
}