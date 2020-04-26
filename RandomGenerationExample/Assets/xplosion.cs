using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xplosion: MonoBehaviour
{
	private Transform player;
	public GameObject destroyeffect;
	private Vector2 target;
	public float lifeTime;
	public LayerMask solid;

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
		Instantiate(destroyeffect, transform.position,Quaternion.identity);
		Destroy(gameObject);
	}

}