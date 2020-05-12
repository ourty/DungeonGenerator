using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    private void Awake()
    {
        current = this;
    }

    public event Action onFloorGeneration;
    public void FloorGeneration()
    {
        if (onFloorGeneration != null)
        {
            onFloorGeneration();
        }
    }
}
