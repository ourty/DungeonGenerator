﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public GameObject destroyEffect;
    public int dmg = 10;
    //public int lifesteal = 0;

    private void Start()
    {
        //GameObject Player = GameObject.Find("Player");
        //PlayerJoystick Hp = Player.GetComponent<PlayerJoystick>();
        Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is hit");
            if (other.name == "Slime")
            {
                other.GetComponent<SlimeEnemy>().TakeDmg(dmg);
            }
            if (other.name == "Slime_R")
            {
                other.GetComponent<SlimeEnemy>().TakeDmg(dmg);
                
            }
            //Hp.currentHealth += lifesteal;
            // other.GetComponent<Enemy>().TakeDmg(dmg);
            DestroyBullet();
        }
        if (other.CompareTag("Walls"))
        {
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
