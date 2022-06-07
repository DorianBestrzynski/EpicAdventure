using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    private float dirX = 0f;

    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private float jumpForce = 14f;

    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {Idle, Running, Jumping, Falling};

    [SerializeField] private AudioSource jumpSoundEffect;

 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        Debug.Log(rb.velocity);

        UpdateAnimationState();

        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("On Move");
        dirX = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
     {
         if (IsGrounded())
         { 
             jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
         }
     }

    private void UpdateAnimationState()
    {
        Debug.Log("Change animation");
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;

        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
