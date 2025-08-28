using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    // Start is called before the first frame update
   private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        anim.Play("Idle");
        rb.isKinematic = true;
        interactAnim.Play("Open");
    }
    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic = false;
    }
}
