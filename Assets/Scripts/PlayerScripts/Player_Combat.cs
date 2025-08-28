using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;       // Titik serangan
    public LayerMask enemyLayers;       // Layer musuh
    public StatsUI statsUI;             // UI untuk status player
    public Animator anim;               // Animator player

    [Header("Attack Settings")]
    public float cooldown = 2f;         // Cooldown serangan
    private float timer;                // Timer internal

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        // Cek musuh di sekitar
        if (attackPoint == null) return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            StatsManager.Instance.weaponRange,
            enemyLayers
        );

        if (enemies.Length > 0)
        {
            Enemy_Health enemyHealth = enemies[0].GetComponent<Enemy_Health>();
            if (enemyHealth != null)
                enemyHealth.changeHealth(-StatsManager.Instance.damage);

            Enemy_Knockback enemyKnockback = enemies[0].GetComponent<Enemy_Knockback>();
            if (enemyKnockback != null)
                enemyKnockback.KnockBack(
                    transform,
                    StatsManager.Instance.knockbackForce,
                    StatsManager.Instance.knockbackTime,
                    StatsManager.Instance.stunTime
                );
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        if (!Application.isPlaying || StatsManager.Instance == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

}
