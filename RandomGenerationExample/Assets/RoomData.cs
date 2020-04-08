using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public char[] doorways;
    private GameObject[] tempFind;
    DoorTemplates doorTemplates;
    void Start()
    {
        tempFind = GameObject.FindGameObjectsWithTag("Templates");
        foreach (GameObject template in tempFind)
        {
            if (template.name == "roomStructureTemplates")
                doorTemplates = template.GetComponent<DoorTemplates>();
        }
    }
    public void SpawnDoors()
    {
        foreach (char door in doorways)
        {
            if (door == 'T')
            {
                Instantiate(doorTemplates.allDoors[0], gameObject.transform.GetChild(2).transform);
            }
            if (door == 'R')
            {
                Instantiate(doorTemplates.allDoors[1], gameObject.transform.GetChild(2).transform);
            }
            if (door == 'B')
            {
                Instantiate(doorTemplates.allDoors[2], gameObject.transform.GetChild(2).transform);
            }
            if (door == 'L')
            {
                Instantiate(doorTemplates.allDoors[3], gameObject.transform.GetChild(2).transform);
            }
        }
    }
}
