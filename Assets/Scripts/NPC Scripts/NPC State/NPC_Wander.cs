using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWidth = 5;
    public float wanderHeight = 5;
    public Vector2 startingPosition;
    public float pauseDuration = 1;
    
    public float speed = 2;
    public Vector2 target;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPaused;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (anim != null)
            anim.SetBool("isWalking", false); // mulai idle
    }


    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(PauseAndPickNewDestination());

        Move();
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        // Perbaikan bagian flip
        if ((direction.x > 0 && transform.localScale.x < 0) ||
            (direction.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        rb.velocity = direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(PauseAndPickNewDestination());
    }
    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        if (anim != null) anim.Play("idle");

        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        isPaused = false;

        if (anim != null) anim.Play("walk");
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDestination());
    }

    private Vector2 GetRandomTarget()
    {
        float halfwidth = wanderWidth / 2;
        float halfheight = wanderHeight / 2;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfwidth, Random.Range(startingPosition.y - halfheight, startingPosition.y + halfheight)), // Left
            1 => new Vector2(startingPosition.x + halfwidth, Random.Range(startingPosition.y - halfheight, startingPosition.y + halfheight)), // Right
            2 => new Vector2(Random.Range(startingPosition.x - halfwidth, startingPosition.x + halfwidth), startingPosition.y - halfheight), // Bottom
            3 => new Vector2(Random.Range(startingPosition.x - halfwidth, startingPosition.x + halfwidth), startingPosition.y + halfheight), // Top
            _ => new Vector2(Random.Range(startingPosition.x - halfwidth, startingPosition.x + halfwidth), startingPosition.y + halfheight),
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
}
