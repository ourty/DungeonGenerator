using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponaim : MonoBehaviour
{
	public GameObject bullet;
	public Transform shotPoint;
	private float timeBtwShots;
	public float startTimeBtwShots;

void combat()
    {
    if (timeBtwShots <=0)
    {
        	Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        	timeBtwShots = startTimeBtwShots;
    }

    else
    {
	timeBtwShots -= Time.deltaTime;
    }

	}
}