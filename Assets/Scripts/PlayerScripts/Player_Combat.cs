using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public StatsUI statsUI;
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
        StatsManager.Instance.damage += 1;
        statsUI.UpdateDamage();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayers);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().changeHealth(-StatsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().KnockBack(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }

    }
    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }
}
