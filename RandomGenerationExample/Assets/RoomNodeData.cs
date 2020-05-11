using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodeData : MonoBehaviour
{

    public bool roomInstatiated = false;
    bool pushDoors = false;
    public GameObject connectedRoom;
    public char[] Doors;
    private void Update()
    {
        if(roomInstatiated && !pushDoors){
            connectedRoom.GetComponent<RoomData>().doorways = Doors;
            pushDoors = true;
            connectedRoom.GetComponent<RoomData>().SpawnDoors();
        }
    }
}


