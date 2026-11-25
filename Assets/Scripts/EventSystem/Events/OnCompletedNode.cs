//TODO: Separar las clases en ficheros

public class OnCompletedNode
{
    public string memoryNodeId;
    public OnCompletedNode(string memoryNodeId)
    {
        this.memoryNodeId = memoryNodeId;
    }
}

public class OnDropMemoryFact
{
    public MemoryFact memoryFact;
    public OnDropMemoryFact(MemoryFact memoryFact)
    {
        this.memoryFact = memoryFact;
    }
}

public class OnCompletedLevel
{
    public LevelData levelData;
    public OnCompletedLevel(LevelData levelData)
    {
        this.levelData = levelData;
    }
}

public class OnCompletedRun
{
    public MemoryRun memoryRun;
    public OnCompletedRun(MemoryRun memoryRun)
    {
        this.memoryRun = memoryRun;
    }
}

public class OnLoadLevel
{
    public string levelId;
    public OnLoadLevel(string levelId)
    {
        this.levelId = levelId;
    }
}