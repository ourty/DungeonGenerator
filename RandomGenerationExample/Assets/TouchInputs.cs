using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputs : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    AttackJoystick aStick;
    PlayerJoystick pStick;
    bool isPlayerStickBeingUsed = false;
    bool isAttackStickBeingUsed = false;
    GameObject aButton;
    private void Start()
    {
        pStick = player.GetComponent<PlayerJoystick>();
        aStick = gun.GetComponent<AttackJoystick>();
    }
    void Update()
    {
        //test
        // if (Input.GetMouseButtonDown(0))
        // {
        //     //Get the mouse position on the screen and send a raycast into the game world from that position.
        //     Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        //     //If something was hit, the RaycastHit2D.collider will not be null.
        //     if (hit.collider != null)
        //     {
        //         Debug.Log(hit.collider.tag);
        //     }
        // }
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((t.position)), Vector2.zero);
                    if (hit.collider == null || !hit.collider.CompareTag("UIButtons"))
                    {
                        if (!isPlayerStickBeingUsed)
                        {
                            isPlayerStickBeingUsed = true;
                            pStick.finger = t;
                            pStick.initializeStick();
                            pStick.touchStart = true;
                            pStick.fingerID = t.fingerId;
                        }
                    }
                    else if (hit.collider.CompareTag("UIButtons"))
                    {
                        if (!isAttackStickBeingUsed)
                        {
                            isAttackStickBeingUsed = true;
                            aStick.finger = t;
                            aStick.initializeStick();
                            hit.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                            aButton = hit.collider.gameObject;
                            aStick.timeBtwShots = aStick.startTimeBtwShots;
                            aStick.touchStart = true;
                            aStick.fingerID = t.fingerId;
                        }
                    }
                }
                else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                {
                    if (pStick.fingerID == t.fingerId)
                    {
                        isPlayerStickBeingUsed = false;
                        pStick.touchStart = false;
                    }
                    if (aStick.fingerID == t.fingerId)
                    {
                        isAttackStickBeingUsed = false;
                        aButton.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                        aStick.touchStart = false;
                    }
                }
                else if (t.phase == TouchPhase.Moved)
                {
                }
            }
        }
    }
}
