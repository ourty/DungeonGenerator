using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private bool moving = false;
    public Transform innerCircle;
    public Transform outerCircle;
    public Rigidbody2D rb;
    Vector2 direction = new Vector2(0f,0f);
    public float offsetaim;
    public GameObject bullet;
    public Transform shotPoint;
    private Transform aimTransform;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public battlebutton button;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
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
            direction = new Vector2(0f,0f);
            innerCircle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
        //Aiming 
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0f,0f, rotZ + offsetaim);

        //****************************************
        Combat();
        move();
    }
    private void FixedUpdate() {
        

        if (moving)
            moveCharacter(direction.normalized);

    }
    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
    void move(){
        rb.velocity = new Vector2(direction.normalized.x, direction.normalized.y);
    }
    void Combat()
    {
        if (timeBtwShots <=0)
        {
        if (Input.GetKey(KeyCode.Space)|| button.isPressing)
        {
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            timeBtwShots = startTimeBtwShots;
        }
    }
               else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}