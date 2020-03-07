using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawners : MonoBehaviour
{
    public List<GameObject> spawnerList = new List<GameObject>();
    private int rand;
    //public bool spawning = false;
    private bool allSpawnersCondensed = true;

    void Update()
    {
        if (!IsInvoking("InvokeSpawn"))
            foreach (GameObject spawner in spawnerList)
            {
                allSpawnersCondensed = true;
                if (!spawner.GetComponent<RoomSpawner>().absorbed)
                {
                    allSpawnersCondensed = false;
                }
            }
        if (spawnerList.Count != 0 && allSpawnersCondensed && !IsInvoking("InvokeSpawn"))
        {

            //spawning = true;
            Invoke("InvokeSpawn", 0.41f);
            // rand = Random.Range(0, spawnerList.Count);
            // spawnerList[rand].GetComponent<RoomSpawner>().chosen = true;
            // spawnerList[rand].GetComponent<RoomSpawner>().Spawn();
        }
    }

    void InvokeSpawn()
    {
        if (spawnerList.Count != 0)
        {
            rand = Random.Range(0, spawnerList.Count);
            if(spawnerList[rand].GetComponent<RoomSpawner>().absorbed)
            spawnerList[rand].GetComponent<RoomSpawner>().Spawn();
        }
    }
    // IEnumerator generate(float time){
    //     rand = Random.Range(0, spawnerList.Count-1);
    //     spawnerList[rand].GetComponent<roomSpawner>().Spawn();
    //     yield return new WaitForSeconds(time+0.1f);
    // }
}

