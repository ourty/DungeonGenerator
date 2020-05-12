using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlimeKing : MonoBehaviour
{
    public int health;
    public float speed;
    public int dmg;
    public float retreatDistance;
    private float timeBtwShots = 1;
    public float startTimeBtwShots;
    public GameObject projectile;
    public Transform player;
    public bool isFlipped = false;
    public GameObject slimeKingSplit;
    public GameObject healthBar;
    Slider slider;
    public Transform shotpoint;
    bool isCreated = false;
    bool shooting = true;

    //Animator
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slider = Instantiate(healthBar, GameObject.Find("HealthBars").transform).GetComponent<Slider>();
    }


    void Update()
    {
        //Will be always facing player and some enemies distance themselves.
        LookPlayer();
        if (shooting == true)
        {
            if (timeBtwShots <= 0)
            {
                animator.SetTrigger("attack");
                Instantiate(projectile, shotpoint.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        slider.value = health;

        //boom boom effect
        if (health <= 0)
        {
            shooting = false;
            if (!isCreated)
            {
                Instantiate(slimeKingSplit, transform.position, transform.rotation, gameObject.transform.parent.transform);
                isCreated = true;
                animator.SetTrigger("death");
                Invoke("DestroyBoss", 0);
            }
        }
    }

    public void LookPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDmg(int dmg)
    {
        health -= dmg;
    }
    void DestroyBoss()
    {
        Destroy(slider.gameObject);
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player is hurt");
            animator.SetTrigger("attack");
            col.gameObject.GetComponent<PlayerJoystick>().TakeDmg(dmg);
        }
    }
}


