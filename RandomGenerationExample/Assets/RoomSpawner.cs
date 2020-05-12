using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    GameObject[] tempFind;
    public List<char> openingNeeded = new List<char>();
    public List<char> closingNeeded = new List<char>();
    private List<Collider2D> collidingList = new List<Collider2D>();
    private RoomTemplates templates;
    private int rand1;
    private bool spawned = false;
    private ActiveSpawners activeSpawners;
    private List<GameObject> roomOptions = new List<GameObject>();

    void Awake()
    {
        activeSpawners = GameObject.Find("/MiniMap/FloorGeneration(Clone)/StartRoom").GetComponent<ActiveSpawners>();
        tempFind = GameObject.FindGameObjectsWithTag("Templates");
        foreach (GameObject template in tempFind)
        {
            if (template.name == "roomNodeTemplates")
                templates = template.GetComponent<RoomTemplates>();
        }
        activeSpawners.spawnerList.Add(this.gameObject);
    }
    void Update()
    {
        if (spawned)
        {
            activeSpawners.spawnerList.Remove(this.gameObject);
            activeSpawners.spawning = false;
            Destroy(this.gameObject);
        }
    }
    public void Spawn()
    {
        if (collidingList.Count != 0)
        {
            for (int i = collidingList.Count - 1; i >= 0; i--)
            {
                if (collidingList[i].CompareTag("SpawnPoint") || collidingList[i].CompareTag("ClosedPoint"))
                {
                    if (collidingList[i].CompareTag("SpawnPoint"))
                    {
                        this.openingNeeded.AddRange(collidingList[i].GetComponent<RoomSpawner>().openingNeeded);
                        activeSpawners.spawnerList.Remove(collidingList[i].gameObject);
                    }
                    else if (collidingList[i].CompareTag("ClosedPoint"))
                    {
                        if (collidingList[i].CompareTag("ClosedPoint"))
                        {
                            this.closingNeeded.AddRange(collidingList[i].GetComponent<RoomClosed>().closingNeeded);
                        }
                    }
                    Destroy(collidingList[i].gameObject);
                }
            }
        }
        bool missingOpening;
        List<GameObject> tempRoomOptions;
        bool missingClosing;

        foreach (GameObject room in templates.allRooms)
        {
            missingOpening = false;
            foreach (char opening in openingNeeded)
            {
                if (room.name.IndexOf(opening) == -1)
                    missingOpening = true;
            }
            if (!missingOpening)
                roomOptions.Add(room);
        }
        tempRoomOptions = new List<GameObject>(roomOptions);
        foreach (GameObject room in tempRoomOptions)
        {
            missingClosing = false;
            foreach (char closing in closingNeeded)
            {
                if (room.name.IndexOf(closing) != -1)
                    missingClosing = true;
            }
            if (missingClosing)
                roomOptions.Remove(room);
        }
        int potentialAndExistingRooms = activeSpawners.roomCount + activeSpawners.spawnerList.Count;
        if ((activeSpawners.maxRooms - potentialAndExistingRooms) < 3)
        {
            if (((activeSpawners.maxRooms - potentialAndExistingRooms) + openingNeeded.Count) < 4) //this if statement may be extra and uneeded
            {
                tempRoomOptions = new List<GameObject>(roomOptions);
                foreach (GameObject room in tempRoomOptions)
                {
                    string name = room.name;
                    if (name.Length > ((activeSpawners.maxRooms - potentialAndExistingRooms) + openingNeeded.Count))
                        roomOptions.Remove(room);
                }
            }
        }
        if ((activeSpawners.maxRooms - potentialAndExistingRooms) == 1)
        {
            Instantiate(roomOptions[1], transform.position, transform.rotation, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent);
            activeSpawners.roomCount++;
            spawned = true;
        }
        else
        {
            rand1 = Random.Range(0, roomOptions.Count);
            Instantiate(roomOptions[rand1], transform.position, transform.rotation, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent);
            activeSpawners.roomCount++;
            spawned = true;
        }

    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("DataPoint"))
        {
            spawned = true;
        }
        else if (hit.CompareTag("SpawnPoint") || hit.CompareTag("ClosedPoint"))
        {
            collidingList.Add(hit);
        }

    }
}
