using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    private Vector2 vel;
    private float baseY;

    void Start()
    {
        baseY = transform.position.y;
    }

    void Update()
    {
    	floating();
    }
    void floating()
    {
    	float targetY = Mathf.PingPong(Time.time * 0.2f, 0.4f);
        Vector2 targetPos = new Vector2(transform.position.x, baseY + targetY);
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref vel, 1f);
    }
}
