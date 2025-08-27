using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int facingDirection = 1; // 1 for right, -1 for left
    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockBack;
    

    public Player_Combat player_Combat;

    private void Update()
    {
        if(Input.GetButtonDown("Slash"))
        {
            player_Combat.Attack();
        }
    }
    void FixedUpdate()
    {
        if (!isKnockBack)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Balik arah player jika bergerak ke arah berlawanan dari skala saat ini
            if ((horizontal > 0 && transform.localScale.x < 0) ||
                (horizontal < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            // Set animasi gerakan
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            // Gerakkan pemain
            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void KnockBack(Transform enemy,float force,float stunTime)
    {
        isKnockBack = true;

        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force; // Bisa disesuaikan knockback speed-nya
        StartCoroutine(KnockBackCounter(stunTime)); // Durasi knockback, bisa disesuaikan
    }
    IEnumerator KnockBackCounter(float stunTime)

    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero; // Hentikan gerakan knockback
        isKnockBack = false; 
    }
}
