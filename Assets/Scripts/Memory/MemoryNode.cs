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

    private GameEventBus eventBus;
    
    [Header("Node data")]
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private NodeType nodeType;
    
    [Header("Locking")]
    //si un nodo esta bloqueado, no se puede acceder a el ni ver el iconito hasta que se desbloquee
    [SerializeField] private bool isLocked;
    
    [Header("Completion")]
    [SerializeField] private bool isCompleted;
    [SerializeField] private MemoryFact providedFactOnComplete;
    
    public string Id => id;
    public NodeType ThisNodeType => nodeType;
    public bool IsLocked => isLocked;
    public bool IsCompleted => isCompleted;
    public MemoryFact ProvidedFactOnComplete => providedFactOnComplete;

    public void EnterNode(GameEventBus eventBus)
    {
        this.eventBus = eventBus;
        this.eventBus.Subscribe<OnCompletedLevel>(CompleteNode);
    }

    //A esto se llamara desde memoryRun cuando se vaya a este nodo,
    //ya sea de normal o cuando se tenga un MemoryFact que desbloquee un nodo bloqueado
    public void UnlockNode()
    {
        isLocked = false;
    }
    
    //Le avisa el level handler. Completar un nivel == completar su nodo correspondiente. El nodo avisa a la run
    
    //El aviso llega al nodo correcto ya que al llamar EnterNode se hace mediante el id del nivel que esta activo.
    //Podria añadirse otra capa de seguridad para unir nodo con nivel, pero de momento se queda asi
    public void CompleteNode(OnCompletedLevel ev)
    {
        if (providedFactOnComplete)
            eventBus.Raise(new OnDropMemoryFact(providedFactOnComplete));
        
        isCompleted = true;
        eventBus.Raise(new OnCompletedNode(id));
        
        //Desuscribo para que no se llame a un CompleteNode equivocado si se completa otro nivel
        eventBus.Unsubscribe<OnCompletedLevel>(CompleteNode);
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
