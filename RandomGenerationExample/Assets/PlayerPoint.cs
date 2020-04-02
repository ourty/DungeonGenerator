using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public Transform parent;
    int rand;
    GameObject[] tempFind;
    RoomTemplates templates;
    ActiveSpawners activeSpawners;
    Transform Rooms;//used to parent to the Rooms GameObject
    bool firstRoomSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        tempFind = GameObject.FindGameObjectsWithTag("Templates");
        foreach (GameObject template in tempFind)
        {
            if (template.name == "roomStructureTemplates")
                templates = template.GetComponent<RoomTemplates>();
        }
        activeSpawners = GameObject.Find("/MiniMap/FloorGeneration/StartRoom").GetComponent<ActiveSpawners>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DataPoint"))
        {
            if (firstRoomSpawn)
            {
                rand = Random.Range(0,templates.allRooms.Length);
                Instantiate(templates.allRooms[rand], new Vector3(0f,0f,0f), templates.allRooms[rand].transform.rotation, parent);
            }
        }
    }
}
