using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private string id = Guid.NewGuid().ToString();
    
    //Aqui habria que meter mas cosas pero de momento,
    //al no saber como seria el juego en si, lo dejo asi como esta
}
