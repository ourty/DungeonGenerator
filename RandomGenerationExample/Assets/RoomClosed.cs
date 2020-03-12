using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomClosed : MonoBehaviour
{
    public List<char> closingNeeded = new List<char>();
    private bool insideExistingRoom = false;
    void Update()
    {
        if (insideExistingRoom)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("DataPoint"))
        {
            insideExistingRoom = true;
        }
    }
}
