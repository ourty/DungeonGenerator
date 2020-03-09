using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawners : MonoBehaviour
{
    public List<GameObject> spawnerList = new List<GameObject>();
    public bool spawning = false;
    private int rand;

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

