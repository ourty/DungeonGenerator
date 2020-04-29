using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float speed = 4.5f;
    public Rigidbody2D rb;
    Vector2 movement; 
    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        //Requires input but will change to motion for movement 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (currentHealth <=0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
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
            TakeDamage(10);

        }
        if (col.gameObject.tag == "PowerUp")
        {
            Debug.Log("PoweredUp");
            Destroy(col.gameObject);
            speed =+ 1;
        }

    }
}
