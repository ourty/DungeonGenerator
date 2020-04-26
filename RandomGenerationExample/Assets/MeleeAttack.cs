using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	private float timeBtwAttack;
	public float startTimeBtwAttack;
	public float offset;
	private Transform aimTransform;
	public Transform attackPos;
	public LayerMask whatIsEnemies;
	public float attackRange;
	public int dmg;

	private void Awake()
	{
		aimTransform = transform.Find("AimMelee");
	}

    void Update()
    {
    	//follows the mouse but will make alternative circle for aim 
    	Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0f,0f, rotZ + offset);
    	
    	if(timeBtwAttack <= 0)
    	{
    	if (Input.GetKey(KeyCode.Space))
    	{
    		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
    		for (int i = 0; i < enemiesToDamage.Length; i++)
    		{
    			enemiesToDamage[i].GetComponent<Enemy>().TakeDmg(dmg);
    		}
    	}
    		timeBtwAttack = startTimeBtwAttack;

    	}
    	else
    	{
    		timeBtwAttack -= Time.deltaTime;
    	}
    }
    //Melee hitbox radius 
    void OnDrawGizmosSelected()
    {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPos.position, attackRange); 
    }
}
