using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] powerups;
    public RoomData Data;
    public Transform SpawnLocation;
    public int Enemies;
    public bool isSpawned = false;

    // Update is called once per frame

    void Update()
    {
    	Enemies = Data.enemiesAlive;
        if (Enemies == 0)
        {
        	if (isSpawned == false)
        	{
        	Instantiate(powerups[Random.Range(0,powerups.Length)], SpawnLocation.position,SpawnLocation.rotation);
        	isSpawned = true;
        	}
        }
    }
}
