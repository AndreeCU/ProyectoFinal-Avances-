using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class GameEventListeners : MonoBehaviour
{
    public GameEvents eventsss;

    public Action response;
    public Action<int> responseInt;
    public Action<float> responseFloat;

    private void OnEnable()
    {
        if (eventsss != null)
            eventsss.Register(this);
    }
    private void OnDisable()
    {
        if (eventsss != null)
            eventsss.Unregister(this);
    }
    public void OnEventRaise()
    {
        response?.Invoke();
    }
    public void OnEventRaise(int i)
    {
        responseInt?.Invoke(i);
    }
    public void OnEventRaise(float i)
    {
        responseFloat?.Invoke(i);
    }
 
}