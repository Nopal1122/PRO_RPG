using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Script untuk mengatur pergerakan musuh yang mengejar pemain saat masuk area trigger
public class Enemy_Movement : MonoBehaviour
{
    public float speed;                    // Kecepatan gerak musuh
    public float attackRange = 2;
    public float attackCooldown = 1;  // Waktu cooldown antara serangan musuh
    public float playerDetectRange = 5; // Jarak deteksi pemain oleh musuh
    public Transform detectionPoint; // Titik deteksi pemain oleh musuh
    public LayerMask playerLayer; // Layer yang digunakan untuk mendeteksi pemain

    private float attackCooldownTimer; // Timer untuk cooldown serangan musuh
    private EnemyState enemyState, newState;         // Status saat ini dari musuh (Idle, Chasing, dll)
    private int facingDirection = -1;      // Arah hadap musuh: -1 ke kiri, 1 ke kanan


    private Rigidbody2D rb;                // Komponen fisika Rigidbody2D musuh
    private Transform player;               // Transform pemain
    private Animator anim;                 // Komponen Animator untuk animasi musuh

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();  // Ambil komponen Rigidbody2D pada musuh
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);       // Atur status awal musuh menjadi Idle// Ambil komponen Animator pada musuh
    }

    void Update()
    {
        if (enemyState != EnemyState.Knockback){
            CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime; // Kurangi timer cooldown serangan
        }
        if (enemyState == EnemyState.Chasing)
        {
            Chase(); // Panggil fungsi Chase jika status musuh adalah Chasing
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero; // Hentikan gerakan musuh saat menyerang
        }
        }
    }

    void Chase()
    {
       

        // Cek apakah pemain berada di arah berlawanan dengan arah hadap musuh
         if (player.position.x > transform.position.x && facingDirection == -1 ||
                       player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip(); // Balik arah musuh
        }
        // Hitung arah menuju pemain dan gerakkan musuh ke arah tersebut
        Vector2 directtion = (player.position - transform.position).normalized;
        rb.velocity = directtion * speed;
    }

    // Membalik arah hadap musuh
    void Flip()
    {
        facingDirection *= -1; // Ubah nilai arah hadap (1 jadi -1 atau sebaliknya)
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Saat pemain masuk ke area trigger musuh
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position,playerDetectRange,playerLayer); // Ambil semua collider dalam area trigger
        if (hits.Length > 0)
        {
            player = hits[0].transform; // Ambil transform pemain pertama yang terdeteksi
            // Jika pemain berada dalam jarak serangan dan cooldown serangan sudah habis
            if (Vector2.Distance(transform.position, player.position) <= attackRange & attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown; // Reset timer cooldown serangan
                ChangeState(EnemyState.Attacking); // Ubah status ke Attacking jika dalam jarak serangan


            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing); // Ubah status ke Chasing jika pemain berada di luar jarak serangan

            }
        }
        else
        {
            rb.velocity = Vector2.zero;     // Hentikan gerakan musuh
            ChangeState(EnemyState.Idle); // Ubah status ke Idle jika tidak ada pemain yang terdeteksi
        }   
        }
   

   
    // Mengubah status musuh dan mengatur animasi sesuai status
    public void ChangeState(EnemyState newState)
    {
        // Matikan animasi dari status sebelumnya
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        // Ubah status musuh
        enemyState = newState;

        // Aktifkan animasi berdasarkan status baru
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Warna Gizmos untuk area deteksi pemain
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange); // Gambar lingkaran deteksi pemain 
    }

}


// Enum untuk mendefinisikan status-status musuh
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}
