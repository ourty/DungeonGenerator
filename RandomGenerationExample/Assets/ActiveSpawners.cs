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

    private void FixedUpdate()
    {
        if (spawnerList.Count != 0 && !spawning)
        {
            spawning = true;
            Invoke("InvokeSpawn", 0.1f);
        }
    }

    void InvokeSpawn()
    {
        if (spawnerList.Count != 0)
        {
            rand = Random.Range(0, spawnerList.Count);
            spawnerList[rand].GetComponent<RoomSpawner>().Spawn();
        }
    }
    // IEnumerator generate(float time){
    //     rand = Random.Range(0, spawnerList.Count-1);
    //     spawnerList[rand].GetComponent<roomSpawner>().Spawn();
    //     yield return new WaitForSeconds(time+0.1f);
    // }
}

