using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler
{
    private GameEventBus eventBus;
    private Dictionary<string, LevelData> nodeIdToLevelData = new();

    //TODO: Llenar en el constructor todos los levelData que haya en el proyecto, y mapearlos a su id de nodo correspondiente
    public LevelHandler(string startNodeId, GameEventBus eventBus)
    {
        this.eventBus = eventBus;
        this.eventBus.Subscribe<OnLoadLevel>(LoadLevel);
    }

    private void LoadLevel(OnLoadLevel ev)
    {
        LevelData levelData = nodeIdToLevelData[ev.levelId];
    }

    //Al completar un nivel se avisa al nodo. El nodo se completa y pasa su informacion a memory run.
    //Quiza podria unirse level y nodo pero de momento lo dejo separado
    private void LevelCompleted(LevelData levelData)
    {
        string id = levelData.name; //suponiendo que el nombre del levelData es el mismo que el id del nodo
        string nodeId = nodeIdToLevelData[id].name;
        eventBus.Raise(new OnCompletedLevel(levelData));
    }
}
