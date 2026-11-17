using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MemoryNode))]
public class EditorUtils : Editor
{
    /*
    public override void OnInspectorGUI()
    {
        var script = (MemoryNode)target;
        script.isLocked = EditorGUILayout.Toggle("Is Locked", script.isLocked);

        if (script.isLocked == false)
            return;
        
        script.keyToUnlock = EditorGUILayout.ObjectField("Key To Unlock", script.keyToUnlock, typeof(MemoryNodeKey), false) as MemoryNodeKey;
    }
    */
}
