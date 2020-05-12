using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] powerups;
    RoomData RData;
    public bool isSpawned = false;

    // Update is called once per frame

    void Update()
    {
        if (RData.enemiesAlive == 0)
        {
        	if (isSpawned == false)
        	{
        	Instantiate(powerups[Random.Range(0,powerups.Length)]);
        	isSpawned = true;
        	}
        }
    }
}
