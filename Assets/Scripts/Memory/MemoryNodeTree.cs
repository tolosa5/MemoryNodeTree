using System.Collections.Generic;
using UnityEngine;

public class MemoryNodeTree : MonoBehaviour
{
    private MemoryNode currentNode;
    private MemoryNode[] memoryNodes;
    private List<MemoryNode> unlockedMemoryNodes = new List<MemoryNode>();
}
