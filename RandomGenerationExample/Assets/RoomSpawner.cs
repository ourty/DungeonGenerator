using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public List<char> openingNeeded = new List<char>();
    public List<char> closingNeeded = new List<char>();
    private List<Collider2D> collidingList = new List<Collider2D>();
    private RoomTemplates templates;
    private int rand1;
    private float rand2;
    private bool spawned = false;
    //public bool chosen = false;
    public bool absorbed = false;
    private Coroutine waiting;
    private ActiveSpawners activeSpawners;
    private List<GameObject> roomOptions = new List<GameObject>();

    //called on start
    void Awake()
    {
        activeSpawners = GameObject.Find("StartRoom").GetComponent<ActiveSpawners>();
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<RoomTemplates>();
        rand2 = Random.Range(0.00f,0.10f);
        StartCoroutine(nonAbsorbCheck());
        activeSpawners.spawnerList.Add(this.gameObject);
    }
    void Update()
    {
        if (spawned)
        {
            activeSpawners.spawnerList.Remove(this.gameObject);
            //activeSpawners.spawning = false;
            Destroy(this.gameObject);
        }
    }
    public void Spawn()
    {
        if(!spawned)
        {
            switch (openingNeeded.Count)
            {
                case 1:
                    foreach (GameObject room in templates.allRooms)
                    {
                        if (room.name.IndexOf(openingNeeded[0]) != -1)
                            roomOptions.Add(room);
                    }
                    break;
                case 2:
                    foreach (GameObject room in templates.allRooms)
                    {
                        if (room.name.IndexOf(openingNeeded[0]) != -1 && room.name.IndexOf(openingNeeded[1]) != -1)
                            roomOptions.Add(room);
                    }
                    break;
                case 3:
                    foreach (GameObject room in templates.allRooms)
                    {
                        if (room.name.IndexOf(openingNeeded[0]) != -1 && room.name.IndexOf(openingNeeded[1]) != -1 && room.name.IndexOf(openingNeeded[2]) != -1)
                            roomOptions.Add(room);
                    }
                    break;
                case 4:
                    foreach (GameObject room in templates.allRooms)
                    {
                        if (room.name.IndexOf(openingNeeded[0]) != -1 && room.name.IndexOf(openingNeeded[1]) != -1 && room.name.IndexOf(openingNeeded[2]) != -1 && room.name.IndexOf(openingNeeded[3]) != -1)
                            roomOptions.Add(room);
                    }
                    break;
            }
            if (closingNeeded.Count != 0)
            {
                List<GameObject> tempRoomOptions = new List<GameObject>(roomOptions);
                switch (closingNeeded.Count)
                {
                    case 1:
                        foreach (GameObject room in tempRoomOptions)
                        {
                            if (room.name.IndexOf(closingNeeded[0]) != -1)
                                roomOptions.Remove(room);
                        }
                        break;
                    case 2:
                        foreach (GameObject room in tempRoomOptions)
                        {
                            if (room.name.IndexOf(closingNeeded[0]) != -1 && room.name.IndexOf(closingNeeded[1]) != -1)
                                roomOptions.Remove(room);
                        }
                        break;
                    case 3:
                        foreach (GameObject room in tempRoomOptions)
                        {
                            if (room.name.IndexOf(closingNeeded[0]) != -1 && room.name.IndexOf(closingNeeded[1]) != -1 && room.name.IndexOf(closingNeeded[2]) != -1)
                                roomOptions.Remove(room);
                        }
                        break;
                    case 4:
                        foreach (GameObject room in tempRoomOptions)
                        {
                            if (room.name.IndexOf(closingNeeded[0]) != -1 && room.name.IndexOf(closingNeeded[1]) != -1 && room.name.IndexOf(closingNeeded[2]) != -1 && room.name.IndexOf(closingNeeded[3]) != -1)
                                roomOptions.Remove(room);
                        }
                        break;
                }
            }
            rand1 = Random.Range(0, roomOptions.Count);
            Instantiate(roomOptions[rand1], transform.position, roomOptions[rand1].transform.rotation);
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        //if (chosen)
        //{
            if (hit.CompareTag("DataPoint"))
            {
                spawned = true;
            }
            else if(hit.CompareTag("SpawnPoint") || hit.CompareTag("ClosedPoint")){
                StartCoroutine(absorbSpawners(hit,rand2));
            }
        //}
    }
    IEnumerator nonAbsorbCheck(){
        yield return new WaitForSeconds(0.4f);
        absorbed = true;
    }
    IEnumerator absorbSpawners(Collider2D hit, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hit.CompareTag("SpawnPoint"))
        {
            this.openingNeeded.AddRange(hit.GetComponent<RoomSpawner>().openingNeeded);
            activeSpawners.spawnerList.Remove(hit.gameObject);
        }
        else if (hit.CompareTag("ClosedPoint"))
        {
            foreach (char closePoint in hit.GetComponent<RoomClosed>().closingNeeded)
            {
                closingNeeded.Add(closePoint);
            }
            //activeSpawners.spawnerList.Remove(hit.gameObject);
        }
        Destroy(hit.gameObject);
        //StopCoroutine(waiting);
        //waiting = StartCoroutine(nonAbsorbCheck());
    }
}
