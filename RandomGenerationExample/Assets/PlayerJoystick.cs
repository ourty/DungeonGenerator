using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : MonoBehaviour
{
    public Transform player;
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
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerBullet bullet;
    public AttackJoystick gun;
    public Transform holdSlot;
    private Transform buddy;
    public HealthBar healthbar;

    private void Awake()
    {
        if (GameSceneManager.playerCharacter == 1)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 2)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 3)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 4)
        {
            animator = gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<Animator>();
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    void Start()
    {
        bullet.transform.localScale = new Vector3 (2.5f,1f,1f);
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        gun.timeBtwShots = 1 ;
        bullet.dmg = 10;
    }
    // Update is called once per frame
    void Update()
    {
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthbar.SetHealth(currentHealth);
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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
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
            gun.startTimeBtwShots = gun.startTimeBtwShots / 1.25f;
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
            bullet.transform.localScale += new Vector3 (.25f,.25f,.25f);
        }
        
       /*if (col.gameObject.tag == "microwave")
        {
            Debug.Log("Microwave buddy");
            col.gameObject.GetComponent<powerup>().enabled = false;
            //col.gameObject.GetComponent<Microwave>().pickedup = true;
            buddy = col.transform;
            buddy.transform.parent = player;
            buddy.transform.position = holdSlot.transform.position;
        }*/

        if (col.gameObject.tag == "MaxHp")
        {
            Debug.Log("MaxHP increased");
            Destroy(col.gameObject);
            maxHealth += 25;
            if (currentHealth != maxHealth)
            {
            currentHealth += 10;
            }
            healthbar.SetHealth(currentHealth);
        }
        if (col.gameObject.tag == "1stAid")
        {
            Debug.Log("Heal");
            Destroy(col.gameObject);
            currentHealth += 50;
            healthbar.SetHealth(currentHealth);
        }

    }
}