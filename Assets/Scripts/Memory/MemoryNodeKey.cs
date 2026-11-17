using UnityEngine;

[CreateAssetMenu(fileName = "MemoryNodeKey", menuName = "Scriptable Objects/MemoryNodeKey")]
public class MemoryNodeKey : ScriptableObject
{
    [SerializeField] private MemoryNode nodeToUnlock;
}
