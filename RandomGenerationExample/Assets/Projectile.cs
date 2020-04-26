using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed;

	private Transform player;
	private Vector2 target;
	public GameObject destroyeffect;
	public float lifeTime;
	public float distance;
	public LayerMask solid;

	void Start()
	{
		Invoke("DestroyProjectile",lifeTime);
		player = GameObject.FindGameObjectWithTag("Player").transform;

		target = new Vector2(player.position.x, player.position.y);
	}

	void Update()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.up,distance,solid);
		if (hitInfo.collider !=null)
		{
			if (hitInfo.collider.CompareTag("Player"))
			{
				Debug.Log("Player hit");
			}
			DestroyProjectile();
		}
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(transform.position.x == target.x && transform.position.y == target.y)
		{
			DestroyProjectile();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
		DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		Instantiate(destroyeffect, transform.position,Quaternion.identity);
		Destroy(gameObject);
	}

}

