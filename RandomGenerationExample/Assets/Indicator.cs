using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator: MonoBehaviour
{
	private Transform player;
	private Vector2 target;
	public float lifeTime;
	public GameObject fist;
	void Start()
	{
		Invoke("DestroyProjectile",lifeTime);
		player = GameObject.FindGameObjectWithTag("Player").transform;

		target = new Vector2(player.position.x, player.position.y);
	}

	void Update()
	{

		Invoke("Fist",1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
		Invoke("DestroyProjectile",1);
		}
	}

	void DestroyProjectile()
	{

		Instantiate( fist,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}

