using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject[] powerups;
    RoomData RData;
    public bool isSpawned = false;

    private void Start() {
        RData = transform.parent.GetComponent<RoomData>();
    }
    void Update()
    {
        if (RData.enemiesAlive == 0)
        {
        	if (isSpawned == false)
        	{
        	Instantiate(powerups[Random.Range(0,powerups.Length)], transform.position, transform.rotation);
        	isSpawned = true;
        	}
        }
    }
}
