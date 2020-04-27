using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;
    public bool shooting;
    public bool isFlipped = false;
    public GameObject xplosion;

    //Animator
    public Animator animator;

    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        //Animation 
        animator.SetFloat("Speed",transform.position.x);

        //Will be always facing player and some enemies distance themselves.
        LookPlayer();
        if( Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
        	transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
        	transform.position = this.transform.position;
        }

        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
        	transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        //only enemy is shooting enables time between shots
    if(shooting == true)
    {
        if (timeBtwShots <= 0)
        {
        	Instantiate(projectile, transform.position, Quaternion.identity);
        	timeBtwShots = startTimeBtwShots;
        }

        else
        {
        	timeBtwShots -= Time.deltaTime;
        }
    }

    //boom boom effect
    if (health <= 0)
    {
        Instantiate(xplosion, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    }

    public void LookPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale=flipped;
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
        health-= dmg;
    }

}
