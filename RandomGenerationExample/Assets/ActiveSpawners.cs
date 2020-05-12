using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawners : MonoBehaviour
{
    public List<GameObject> spawnerList = new List<GameObject>();
    public bool spawning = false;
    private int rand;
    //this will also keep track of all existing rooms
    public int roomCount = 1;//this should be set to 1 by default as to account for the starting room
    public int maxRooms;
    private void Start()
    {
        //right now max rooms must be at the minimum, 5.
        if (maxRooms < 5)
        {
            maxRooms = 5;
        }
    }
    private void FixedUpdate()
    {
        if (spawnerList.Count != 0 && !spawning)
        {
            spawning = true;
            Invoke("InvokeSpawn", 0.1f);
        }
        if (spawnerList.Count == 0)
        { //remove loading screen when done generating
            Invoke("EndLoadingScreen", 0.2f);
        }
    }

    void InvokeSpawn()
    {
        try
        {
            if (spawnerList.Count != 0)
            {
                rand = Random.Range(0, spawnerList.Count);
                spawnerList[rand].GetComponent<RoomSpawner>().Spawn();
            }
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Debug.Log("Wrong index for spawnerList used");
            spawning = false;
        }
    }
    void EndLoadingScreen()
    {
        EventManager.current.DoneLoading();
    }
}

