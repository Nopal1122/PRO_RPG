using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1;
    public float knockbackForce = 50;
    public LayerMask enemyLayers;

    public int damage = 1;

    public Animator anim;

    public float cooldown = 2;
    private float timer;



    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void attack() {
       

        if(timer <= 0)
        {   
            anim.SetBool("isAttacking", true);
          
            timer = cooldown;
        }
   }
    
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayers);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().changeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().KnockBack(transform, knockbackForce);
        }

    }
    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
