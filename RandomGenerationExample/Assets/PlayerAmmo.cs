using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	public float distance;
	public GameObject destroyEffect;
	public LayerMask whatIsSolid;
	public int dmg;
	private void Start()
	{
		Invoke("DestroyBullet", lifeTime);
	}

	private void Update()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up,distance, whatIsSolid);
		if (hitInfo.collider != null)
		{
			if(hitInfo.collider.CompareTag("Enemy"))
			{
				Debug.Log("Enemy is hit");
				hitInfo.collider.GetComponent<Enemy>().TakeDmg(dmg);
			}
			DestroyBullet();
		}
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

	void DestroyBullet()
	{
		Instantiate(destroyEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
