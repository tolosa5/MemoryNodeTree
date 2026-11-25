using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEventBus", menuName = "Core/GameEventBus")]
public class GameEventBus : ScriptableObject
{
    private readonly Dictionary<Type, Delegate> handlers = new();

    public void Subscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);
        if (handlers.TryGetValue(type, out Delegate value))
            handlers[type] = Delegate.Combine(value, handler);
        else
            handlers[type] = handler;
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);
        if (!handlers.TryGetValue(type, out Delegate value)) return;
        Delegate newDel = Delegate.Remove(value, handler);
        if (newDel == null) handlers.Remove(type);
        else handlers[type] = newDel;
    }

    public void Raise<T>(T evt)
    {
        Type type = typeof(T);
        
        if (handlers.TryGetValue(type, out Delegate value))
            (value as Action<T>)?.Invoke(evt);
    }

    public void Clear()
    {
        handlers.Clear();
    }
}