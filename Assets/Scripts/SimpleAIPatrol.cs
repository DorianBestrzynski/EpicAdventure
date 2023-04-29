using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIPatrol : MonoBehaviour
{

    [HideInInspector] public bool mustPatrol;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float walkSpeed;

    private bool mustTurn, canShoot;


    [SerializeField] private Transform groundCheckPos;

    [SerializeField] private LayerMask groundLayer;

    private enum MovementState { Idle, Running };

    private Animator anim;




    [SerializeField] private Collider2D bodyColider;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementState state;
        if (mustPatrol)
        {
            Patrol();
        }
    }


    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    private void Patrol()
    {
        if (mustTurn || bodyColider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
