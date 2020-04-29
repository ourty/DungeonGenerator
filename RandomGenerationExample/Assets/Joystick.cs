using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 4.0f;
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
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect;
    public PlayerAmmo PlayerProj;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <=0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

    }
    void Slowed()
    {
        speed = 2f;
    }
    void ReturnSpd()
    {
        speed = 4f;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Is hurt");
            TakeDamage(20);
        }

         if (col.gameObject.tag =="Projectile")
        {
            Debug.Log("Is hurt(Projectile");
            TakeDamage(10);
        }

        if (col.gameObject.tag =="ProjectileW")
        {
            Debug.Log("Is hurt(Projectile");
            Slowed();
            Invoke("ReturnSpd",4f);
            TakeDamage(10);

        }
        if (col.gameObject.tag == "SpeedUp")
        {
            Debug.Log("SpedUp");
            Destroy(col.gameObject);
            speed += 1f;
        }
        if (col.gameObject.tag == "PowerUp")
        {
            Debug.Log("AttackSpd");
            Destroy(col.gameObject);
            startTimeBtwShots = startTimeBtwShots / 1.25f;
        }
        if (col.gameObject.tag == "DMGUp")
        {
            Debug.Log("DoubleTap");
            Destroy(col.gameObject);
            PlayerProj.dmg += 10;
        }
        if (col.gameObject.tag == "OneUp")
        {
            Debug.Log("SizeUp");
            Destroy(col.gameObject);
            PlayerProj.transform.localScale += new Vector3 (.25f,.25f,.25f);
        }

    }
}