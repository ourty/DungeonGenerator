using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;
    public bool shoots;
    public bool isFlipped = false;


    //Animator
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {

        //Will be always facing player and some enemies distance themselves.
        LookPlayer();
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetTrigger("moving");
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            animator.SetTrigger("moving");
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            animator.SetTrigger("moving");
        }

        //only enemy is shooting enables time between shots
        if (shoots == true)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
                animator.SetTrigger("attack");
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        //boom boom effect
        if (health <= 0 && !IsInvoking("DestroySpawnPoint"))
        {
            animator.SetTrigger("death");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("DestroySpawnPoint",0.0f);
        }
    }
    void DestroySpawnPoint()
    {
        Destroy(gameObject.transform.parent.gameObject);
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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Is hurt");
            animator.SetTrigger("attack");
        }

    }
}
