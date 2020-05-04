using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJoystick : MonoBehaviour
{
    public Transform gun;
    public bool touchStart = false;
    public Vector2 pointA;
    public Vector2 pointB;
    private bool moving = false;
    public Transform innerCircle;
    public Transform outerCircle;
    //public Rigidbody2D rb;
    Vector2 direction = new Vector2(0f, 0f);
    //public float offsetaim;
    public GameObject bullet;
    public Transform gunbarrel;
    //private Transform aimTransform;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public Touch finger;
    public int fingerID;

    //public GameObject button;

    private void Awake()
    {
        // aimTransform = transform.Find("Aim");
    }
    void Update()
    {
        if (touchStart)
        {
            if (Input.touchCount > 0)
                foreach (Touch t in Input.touches)
                {
                    if (fingerID == t.fingerId)
                    {
                        finger = t;
                    }
                }
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(finger.position.x, finger.position.y, Camera.main.transform.position.z));
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 10.0f);
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
        //Aiming 
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Combat();
        timeBtwShots -= Time.deltaTime;
    }
    public void initializeStick()
    {
        pointA = Camera.main.ScreenToWorldPoint(new Vector3(finger.position.x, finger.position.y, Camera.main.transform.position.z));
        //Debug.Log(pointA.ToString());
        innerCircle.transform.position = pointA;
        outerCircle.transform.position = pointA;
        innerCircle.GetComponent<SpriteRenderer>().enabled = true;
        outerCircle.GetComponent<SpriteRenderer>().enabled = true;
    }
    void Combat()
    {
        if (timeBtwShots <= 0)
        {
            if (touchStart)
            {
                Instantiate(bullet, gunbarrel.position, gunbarrel.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
    }
}
