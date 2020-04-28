using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputs : MonoBehaviour
{
    public GameObject player;
    public GameObject aStick;
    PlayerJoystick pStick;
    bool isPlayerStickBeingUsed = false;
    bool isAttackStickBeingUsed = false;
    int pStickFingerID;
    int aStickFingerID;
    private void Start()
    {
        pStick = player.GetComponent<PlayerJoystick>();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((t.position)), Vector2.zero);
                    if (hit.collider == null)
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
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    if (pStickFingerID == t.fingerId)
                    {
                        isPlayerStickBeingUsed = false;
                        pStick.touchStart = false;
                    }
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    if (pStickFingerID == t.fingerId)
                    {
                        //pStick.pointB = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, Camera.main.transform.position.z));
                    }
                }
            }
        }
    }
}
