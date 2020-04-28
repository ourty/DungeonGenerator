using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public char exit;
    GameObject playerPoint;
    GameObject player;
    PlayerPoint playerPointData;
    GameObject[] tempFind;
    void Start()
    {
        playerPoint = GameObject.FindGameObjectWithTag("PlayerPoint");
        playerPointData = playerPoint.GetComponent<PlayerPoint>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            player = hit.gameObject;
            MoveRooms();
        }
    }

    void MoveRooms()
    {
        switch (exit)
        {
            case 'T':
                playerPointData.currPos.Set(playerPointData.currPos.x, playerPointData.currPos.y + 48);
                playerPoint.transform.Translate(Vector2.up * 10);
                TransitionRooms(new Vector2(0f, 48f), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 12));
                break;
            case 'B':
                playerPointData.currPos.Set(playerPointData.currPos.x, playerPointData.currPos.y - 48);
                playerPoint.transform.Translate(Vector2.down * 10);
                TransitionRooms(new Vector2(0f, -48f), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 12));
                break;
            case 'L':
                playerPointData.currPos.Set(playerPointData.currPos.x - 85.4f, playerPointData.currPos.y);
                playerPoint.transform.Translate(Vector2.left * 10);
                TransitionRooms(new Vector2(-85.4f, 0f), new Vector2(gameObject.transform.position.x - 12, gameObject.transform.position.y));
                break;
            case 'R':
                playerPointData.currPos.Set(playerPointData.currPos.x + 85.4f, playerPointData.currPos.y);
                playerPoint.transform.Translate(Vector2.right * 10);
                TransitionRooms(new Vector2(85.4f, 0f), new Vector2(gameObject.transform.position.x + 12, gameObject.transform.position.y));
                break;
        }
    }
    void TransitionRooms(Vector2 move, Vector2 playerMove)
    {
        player.SetActive(false);
        player.transform.SetPositionAndRotation(playerMove, Quaternion.identity);
        Camera.main.transform.Translate(move);
        player.GetComponent<Joystick>().pointA += move;
        player.GetComponent<Joystick>().outerCircle.Translate(move);
        player.GetComponent<Joystick>().innerCircle.Translate(move);
        player.GetComponent<Joystick>().pointB += move;
        
        player.SetActive(true);
    }
}
