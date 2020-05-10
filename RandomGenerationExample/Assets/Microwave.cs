using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Microwave : MonoBehaviour
{
	public bool pickedup = false;
    private Transform target;
    public float range = 5f;

    public string enemyTag = "Enemy";

    public float timeBtwShots = 1f;
    private float startTimeBtwShots;

    public Transform shotpoint;
    public GameObject projectile; 


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
        	float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        	if (distanceToEnemy < shortestDistance)
        	{
        		shortestDistance = distanceToEnemy;
        		nearestEnemy = enemy;
        	}
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
        	target = nearestEnemy.transform;
        }
        else
        {
        	target = null;
        }
    }

    void Update()
    {
    	if (pickedup == true)
    	{
    		if (target == null)
    		return;

    		if (timeBtwShots <= 0f)
    		{
    		Shoot();
    		timeBtwShots = startTimeBtwShots;
    		}

    		else
    		{
    		timeBtwShots -= Time.deltaTime;
    		}
    	}
    }

    void Shoot()
    {
    	(GameObject)Instantiate(projectile, shotpoint.position, shotpoint.rotation);
    	proprojectile.GetComponent<allyshot>();
    	if (bullet != null)
    	bullet.Seek(target);
    }

    void OnDrawGizmosSelected ()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, range);
    }

}*/
