using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : MonoBehaviour
{
    public int health;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectilef;
    public Transform player;
    public bool shooting;
    public bool isFlipped = false;
    public GameObject xplosion;
    public GameObject deatheff;

    private float timeBtwFist;
    public float startTimeBtwFist;

    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {

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


        if (timeBtwShots <= 0)
        {
        	Invoke("fire",2);
        	timeBtwShots = startTimeBtwShots;
        }

        else
        {
        	timeBtwShots -= Time.deltaTime;
        }
        //projectile underplayer delayed by a timer so player has time to move
        if (timeBtwFist <= 0)
        {
        	
        	Invoke("xplosionatk",2);
        	timeBtwFist = startTimeBtwFist;
        }

        else
        {
        	timeBtwFist -= Time.deltaTime;
        }
          if (health <= 0)
    {
        Instantiate(deatheff, transform.position, Quaternion.identity); 
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
	public void fire()
	{
		Instantiate(projectilef, transform.position, Quaternion.identity);
	}
    
	public void xplosionatk()
	{
			Instantiate(xplosion, player.position,Quaternion.identity);
	}
        public void TakeDmg(int dmg)
    {
        health-= dmg;
    }

}