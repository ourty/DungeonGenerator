using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    public Transform player;
    public int health = 100;
    public float speed = 5.0f;
    public bool touchStart = false;
    public Vector2 pointA;
    public Vector2 pointB;
    private bool moving = false;
    public Transform innerCircle;
    public Transform outerCircle;
    public Rigidbody2D rb;
    private Animator animator;
    Vector2 direction = new Vector2(0f, 0f);
    public Touch finger;
    public int fingerID;
    public AttackJoystick gun;
    public PlayerBullet bullet;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("CharacterSelected") == 1)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 2)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 3)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 4)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            EventManager.current.GameRunEnd();
        }
        if (touchStart)
        {
            if(Input.touchCount > 0)
            foreach (Touch t in Input.touches)
            {
                if(fingerID == t.fingerId){
                    finger = t;
                }
            }
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(finger.position.x, finger.position.y, Camera.main.transform.position.z));
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 8.0f);
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
    public void initializeStick()
    {
        pointA = Camera.main.ScreenToWorldPoint(new Vector3(finger.position.x, finger.position.y, Camera.main.transform.position.z));
        //Debug.Log(pointA.ToString());
        innerCircle.transform.position = pointA;
        outerCircle.transform.position = pointA;
        innerCircle.GetComponent<SpriteRenderer>().enabled = true;
        outerCircle.GetComponent<SpriteRenderer>().enabled = true;
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
    public void TakeDmg(int dmg){
        health -= dmg;
    }
        public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Is hurt");
            TakeDmg(20);
        }

         if (col.gameObject.tag =="Projectile")
        {
            Debug.Log("Is hurt(Projectile");
            TakeDmg(10);
        }

        if (col.gameObject.tag == "SpeedUp")
        {
            Debug.Log("SpedUp");
            Destroy(col.gameObject);
            speed += 2f;
        }
        if (col.gameObject.tag == "PowerUp")
        {
            Debug.Log("AttackSpd");
            Destroy(col.gameObject);
            gun.timeBtwShots /= .25f;

        }
        if (col.gameObject.tag == "DMGUp")
        {
            Debug.Log("DoubleTap");
            Destroy(col.gameObject);
            bullet.dmg += 10;

        }
        if (col.gameObject.tag == "OneUp")
        {
            Debug.Log("SizeUp");
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "MaxHp")
        {
            Debug.Log("MaxHP increased");
            Destroy(col.gameObject);
            health += 50;
        }
        if (col.gameObject.tag == "1stAid")
        {
            Debug.Log("Heal");
            Destroy(col.gameObject);
            health += 25;
        }
        if (col.gameObject.tag == "lifesword")
        {
            Debug.Log("lifesteal");  
            Destroy(col.gameObject);
            health += 20;
            bullet.dmg += 20;
        }

    }
}