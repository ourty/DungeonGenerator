using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    public Vector2 pointA;
    public Vector2 pointB;
    private bool moving = false;
    public Transform innerCircle;
    public Transform outerCircle;
    public Rigidbody2D rb;
    private Animator animator;
    Vector2 direction = new Vector2(0f, 0f);

    private void Start()
    {
        if(GameSceneManager.playerCharacter == 1){
            gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        if(GameSceneManager.playerCharacter == 2){
            gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
        if(GameSceneManager.playerCharacter == 3){
            gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }
        if(GameSceneManager.playerCharacter == 4){
            gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            //Debug.Log(pointA.ToString());
            innerCircle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            innerCircle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 9.0f);
            //Debug.Log(offset.normalized + " " + offset.x + " " + offset.y);
            moving = true;
            innerCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            moving = false;
            direction = new Vector2(0f, 0f);
            innerCircle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
        move();
        animator.SetFloat("front", -(direction.y));
        animator.SetFloat("back", (direction.y));
        animator.SetFloat("left", -(direction.x));
        animator.SetFloat("right", (direction.x));
    }
    private void FixedUpdate()
    {
        if (moving)
            moveCharacter(direction.normalized);
    }
    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
    void move()
    {
        rb.velocity = new Vector2(direction.normalized.x, direction.normalized.y);
    }
}