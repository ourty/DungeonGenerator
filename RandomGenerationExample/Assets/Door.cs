using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public char exit;
    public GameObject PlayerPoint;
    // Update is called once per frame
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
            MoveRooms();
    }

    void MoveRooms()
    {
        switch (exit)
        {
            case 'T':
                break;
            case 'B':
                break;
            case 'L':
                break;
            case 'R':
                break;
        }
    }
}
