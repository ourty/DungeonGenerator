using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allyshot : MonoBehaviour
{
	private Transform target;

	public float speed = 9f;
	public float lifeTime;
	public GameObject destroyeffect;
	public int dmg = 10;

	public void Seek(Transform _target)
	{
		target = _target;
	}

    // Update is called once per frame
    void Update()
    {
		/*transform.position = transform.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(transform.position.x == target.x && transform.position.y == target.y)
		{
			DestroyProjectile();
		}

    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		Instantiate(destroyeffect, transform.position,Quaternion.identity);
		Destroy(gameObject);
	}*/
	if (target == null)
	{
		Destroy(gameObject);
		return;
	}
	Vector3 dir = target.position - transform.position;
	float distanceThisFrame = speed * Time.deltaTime;

	if (dir.magnitude <= distanceThisFrame)
	{
		HitTarget();
		return;
	}
	transform.Translate(dir.normalized*distanceThisFrame, Space.World);
	}
	void HitTarget()
	{
		Debug.Log("HitSomething");
	}
}
