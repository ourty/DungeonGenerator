using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawners : MonoBehaviour
{
    public List<GameObject> spawnerList = new List<GameObject>();
    private GameObject chosenSpawner;
    private int rand;
    //public bool spawning = false;
    private bool allSpawnersCondensed = true;

    void Update()
    {
        // if (!IsInvoking("InvokeSpawn"))
        //     foreach (GameObject spawner in spawnerList)
        //     {
        //         allSpawnersCondensed = true;
        //         if (!spawner.GetComponent<RoomSpawner>().absorbed)
        //         {
        //             allSpawnersCondensed = false;
        //         }
        //     }
        if (spawnerList.Count != 0 && allSpawnersCondensed && !IsInvoking("InvokeSpawn"))
        {
            rand = Random.Range(0, spawnerList.Count);//moved
            // rand = Random.Range(0, spawnerList.Count);
            chosenSpawner = spawnerList[rand];
            chosenSpawner.GetComponent<RoomSpawner>().enabled = true;
            // spawnerList[rand].GetComponent<RoomSpawner>().Spawn();
            Invoke("InvokeSpawn", 0.3f);
        }
    }

    void InvokeSpawn()
    {
        if (spawnerList.Count != 0 && chosenSpawner)
        {
            //moved
            //if (spawnerList[rand].GetComponent<RoomSpawner>().absorbed)
                chosenSpawner.GetComponent<RoomSpawner>().Spawn();
        }
    }
    // IEnumerator generate(float time){
    //     rand = Random.Range(0, spawnerList.Count-1);
    //     spawnerList[rand].GetComponent<roomSpawner>().Spawn();
    //     yield return new WaitForSeconds(time+0.1f);
    // }
}

