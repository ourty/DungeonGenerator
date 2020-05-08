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
    public event Action onDoneLoading;
    public void DoneLoading()
    {
        if (onDoneLoading != null)
        {
            onDoneLoading();
        }
    }
    public event Action onStartLoading;
    public void StartLoading()
    {
        if (onStartLoading != null)
        {
            onStartLoading();
        }
    }
    public event Action<GameObject> onUpdateCurrentRoom;
    public void updateCurrentRoom(GameObject currentRoom)
    {
        if (onUpdateCurrentRoom != null)
        {
            onUpdateCurrentRoom(currentRoom);
        }
    }
    public event Action onGameRunStart;
    public void GameRunStart()
    {
        if (onGameRunStart != null)
        {
            onGameRunStart();
        }
    }
    public event Action onGameRunEnd;
    public void GameRunEnd()
    {
        if (onGameRunEnd != null)
        {
            onGameRunEnd();
        }
    }
}
