using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private float speed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {

    }
    public Animator animator;
    
    void Update()
    {
        animator.SetFloat("front", -(movement.y));
        animator.SetFloat("back", (movement.y));
        animator.SetFloat("left", -(movement.x));
        animator.SetFloat("right", (movement.x));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
