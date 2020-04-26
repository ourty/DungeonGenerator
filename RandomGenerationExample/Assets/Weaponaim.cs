using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponaim : MonoBehaviour
{
	public float offset;
	public GameObject bullet;
	public Transform shotPoint;
	private Transform aimTransform;

	private float timeBtwShots;
	public float startTimeBtwShots;

	private void Awake()
	{
		aimTransform = transform.Find("Aim");
	}

    private void Update()
    {
    	//follows the mouse but will make alternative circle for aim 
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0f,0f, rotZ + offset);

    if (timeBtwShots <=0)
    {
        if (Input.GetMouseButtonDown(0))
        {
        	Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        	timeBtwShots = startTimeBtwShots;
        }
    }
    else
    {
	timeBtwShots -= Time.deltaTime;
    }
	}
}