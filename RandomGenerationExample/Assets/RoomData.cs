using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public char[] doorways;
    private GameObject[] tempFind;
    DoorTemplates doorTemplates;
    public bool currentRoom = false;
    public int enemiesAlive;
    void Start()
    {
        EventManager.current.onUpdateCurrentRoom += updateCurrentRoom;
        tempFind = GameObject.FindGameObjectsWithTag("Templates");
        foreach (GameObject template in tempFind)
        {
            if (template.name == "roomStructureTemplates")
                doorTemplates = template.GetComponent<DoorTemplates>();
        }
    }
    private void Update() {
        enemiesAlive = transform.GetChild(2).transform.childCount;
    }

    public void SpawnDoors()
    {
        foreach (char door in doorways)
        {
            if (door == 'T')
            {
                Instantiate(doorTemplates.allDoors[0], gameObject.transform.GetChild(1).transform);
            }
            if (door == 'R')
            {
                Instantiate(doorTemplates.allDoors[1], gameObject.transform.GetChild(1).transform);
            }
            if (door == 'B')
            {
                Instantiate(doorTemplates.allDoors[2], gameObject.transform.GetChild(1).transform);
            }
            if (door == 'L')
            {
                Instantiate(doorTemplates.allDoors[3], gameObject.transform.GetChild(1).transform);
            }
        }
    }
    private void updateCurrentRoom(GameObject room){
        currentRoom = false;
        if(GameObject.ReferenceEquals(room,gameObject)){
            currentRoom = true;
        }
    }

    int Enemies()
    {
        return enemiesAlive;
    }
}
