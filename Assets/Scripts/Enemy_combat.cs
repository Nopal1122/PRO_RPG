using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint; // Titik serangan
    public float weaponRange; // Jarak serangan
    public float knockBackForce;
    public float stunTime ; // Waktu stun saat terkena serangan
    public LayerMask playerLayer; // Layer pemain

   
    public void Attack()
    {
         Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if(hits.Length > 0)
        {
                    hits[0].GetComponent<PlayerHealh>().ChangeHealth(-damage);
                    hits[0].GetComponent<PlayerMovement>().KnockBack(transform,knockBackForce,stunTime);

        }
            }
        }
    

