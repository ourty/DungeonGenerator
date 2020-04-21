using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    Transform floorRoomParent;
    int rand;
    GameObject[] tempFind;
    RoomTemplates templates;
    ActiveSpawners activeSpawners;
    public Vector2 currPos = new Vector2(0f, 0f); //keeps track of the the current position of the current room in the world space
    bool startRoomInstantiated = false;
    Transform Rooms;//used to parent to the Rooms GameObject

    // Start is called before the first frame update
    void Awake()
    {
        tempFind = GameObject.FindGameObjectsWithTag("Templates");
        foreach (GameObject template in tempFind)
        {
            if (template.name == "roomStructureTemplates")
                templates = template.GetComponent<RoomTemplates>();
        }
        //activeSpawners = GameObject.Find("/MiniMap/FloorGeneration(Clone)/StartRoom").GetComponent<ActiveSpawners>();
        tempFind = GameObject.FindGameObjectsWithTag("RootObjects");
        foreach (GameObject root in tempFind)
        {
            if (root.name == "Floor1Rooms(Clone)")
                floorRoomParent = root.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DataPoint"))
        {
            RoomNodeData rnData = other.GetComponent<RoomNodeData>(); //keeps track of script info pulled
            if (!startRoomInstantiated)
            {
                rand = Random.Range(0, templates.allRooms.Length);
                rnData.connectedRoom = Instantiate(templates.allRooms[rand], new Vector3(0f, 0f, 0f), templates.allRooms[rand].transform.rotation, floorRoomParent) as GameObject;
                rnData.roomInstatiated = true;
                startRoomInstantiated = true;
            }
            else if (!rnData.roomInstatiated)
            {
                rand = Random.Range(0, templates.allRooms.Length);
                rnData.connectedRoom = Instantiate(templates.allRooms[rand], currPos, templates.allRooms[rand].transform.rotation, floorRoomParent) as GameObject;
                rnData.roomInstatiated = true;
            }
        }
    }
}
