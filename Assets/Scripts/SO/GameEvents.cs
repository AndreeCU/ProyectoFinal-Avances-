using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "GameEvents", menuName = "ScriptableObject/GameEvents/Events")]
public class GameEvents : ScriptableObject
{
    private List<GameEventListeners> listeners;

    private void OnEnable()
    {
        listeners = new List<GameEventListeners>();
    }
    public void Raise()
    {
        if (listeners != null)
        {
            foreach (GameEventListeners lis in listeners)
            {
                lis.OnEventRaise();
            }
        }
    }
    public void Raise(int i)
    {
        if (listeners != null)
        {
            foreach (GameEventListeners lis in listeners)
            {
                lis.OnEventRaise(i);
            }
        }
    }
    public void Raise(float i)
    {
        if (listeners != null)
        {
            foreach (GameEventListeners lis in listeners)
            {
                lis.OnEventRaise(i);
            }
        }
    }

    public void Register(GameEventListeners listener)
    {
        listeners.Add(listener);
    }
    public void Unregister(GameEventListeners listener)
    {
        listeners.Remove(listener);
    }
}