using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodeData : MonoBehaviour
{

    public bool roomInstatiated = false;
    bool pushOpenings = false;
    public GameObject connectedRoom;
    public char[] Openings;
    private void Update()
    {
        if(roomInstatiated && !pushOpenings){
            connectedRoom.GetComponent<RoomData>().doorways = Openings;
            pushOpenings = true;
            connectedRoom.GetComponent<RoomData>().SpawnDoors();
        }
    }
}


