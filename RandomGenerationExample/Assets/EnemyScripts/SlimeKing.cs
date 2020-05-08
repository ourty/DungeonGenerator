using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlimeKing : MonoBehaviour
{
    public int health;
    public float speed;
    public float retreatDistance;

    private float timeBtwShots = 1;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;
    public bool isFlipped = false;
    public GameObject slimebb1;
    public GameObject slimebb2;
    public Transform slimespawns1;
    public Transform slimespawns2;
    public Transform slimespawns3;
    public Transform slimespawns4;
    public Slider healthBar;
    public Transform shotpoint;
    bool isCreated = false;
    bool shooting = true;

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
    	healthBar.value= health;

    //boom boom effect
    if (health <= 0)
    	{
            shooting = false;
            if (isCreated == false)
            {
    	   Instantiate(slimebb1, slimespawns1.position,Quaternion.identity);
    	   Instantiate(slimebb2, slimespawns2.position,Quaternion.identity);
           Instantiate(slimebb1, slimespawns3.position,Quaternion.identity);
           Instantiate(slimebb2, slimespawns4.position,Quaternion.identity);
           isCreated = true;

            }
        animator.SetTrigger("death");
        Invoke("DestroyBoss",1.65f);
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
    void DestroyBoss()
    {
        Destroy(gameObject);
    }

}


