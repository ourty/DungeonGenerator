using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public char exit;
    GameObject playerPoint;
    GameObject player;
    GameObject gun;
    PlayerPoint playerPointData;
    GameObject[] tempFind;
    RoomData parentRD;
    public Sprite[] sprites; //open = 0 closed = 1
    Animator animator;
    void Start()
    {
        playerPoint = GameObject.FindGameObjectWithTag("PlayerPoint");
        playerPointData = playerPoint.GetComponent<PlayerPoint>();
        parentRD = transform.parent.transform.parent.GetComponent<RoomData>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            player = hit.gameObject;
            gun = player.transform.GetChild(1).gameObject;
            MoveRooms();
        }
    }
    private void Update()
    {
        if (parentRD.enemiesAlive > 0 && parentRD.currentRoom)
        {
            animator.SetBool("closeDoor",true);
        }
        else
            animator.SetBool("closeDoor",false);

        if(animator.GetBool("closeDoor")){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else{
            gameObject.GetComponent<BoxCollider2D>().enabled = true;;
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
        player.GetComponent<PlayerJoystick>().pointA += move;
        gun.GetComponent<AttackJoystick>().pointB += move;
        gun.GetComponent<AttackJoystick>().pointA += move;
        player.GetComponent<PlayerJoystick>().pointB += move;

        player.SetActive(true);
    }
}
