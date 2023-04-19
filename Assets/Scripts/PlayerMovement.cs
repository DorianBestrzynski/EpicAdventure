using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    private float dirX = 0f;

    private SpriteRenderer sprite;
    
    private float magicianSpeed = 8f;

    private float knightSpeed = 6f;


    [SerializeField] private float magicianJump = 16f;

    [SerializeField] private float knightJump = 14f;

    [SerializeField] private int magicianHealth= 2;

    [SerializeField] private int knightHealth = 4;

    private PlayerInput playerInput;


    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {Idle, Running, Jumping, Falling};

    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] public GameObject player1, player2;
    [SerializeField] private AudioSource attackSoundEffect;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        playerInput = GetComponent<PlayerInput>();
      

        Debug.Log("hardness level" + StaticVariables.isEasyMode);
        if (StaticVariables.isPLayerOneMagician)
        {
            StaticVariables.playerOneSpeed = magicianSpeed;
            StaticVariables.playerOneJumpForce = magicianJump;
 

        }
        else
        {
            StaticVariables.playerOneSpeed = knightSpeed;
            StaticVariables.playerOneJumpForce = knightJump;
            Vector3 local = transform.localScale;
           
        }

        if (StaticVariables.isPLayerTwoMagician)
        {
            StaticVariables.playerTwoSpeed = magicianSpeed;
            StaticVariables.playerTwoJumpForce = magicianJump;

        }
        else
        {
            StaticVariables.playerTwoSpeed = knightSpeed;
            StaticVariables.playerTwoJumpForce = knightJump;
            StaticVariables.playerTwoLife = knightHealth;
        
        }

        if (transform.gameObject.CompareTag("Player") && StaticVariables.isPLayerOneMagician)
        {
            playerInput.SwitchCurrentActionMap("Player2");
            StaticVariables.hasSwithed = true;
        }
        if (transform.gameObject.CompareTag("Player2") && !StaticVariables.isPLayerTwoMagician)
        {
            playerInput.SwitchCurrentActionMap("Player");
            StaticVariables.hasSwithed = true;
        }

        if (transform.gameObject.CompareTag("Player") && !StaticVariables.isPLayerOneMagician)
        {
         //   transform.localScale = new Vector3(5f, 5f, 1);
        }
        else if (transform.gameObject.CompareTag("Player2") && !StaticVariables.isPLayerTwoMagician)

        {
          //  transform.localScale = new Vector3(5f, 5f, 1);
        }


    }

    private void Update()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        
        if ((transform.gameObject.CompareTag("Player") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player2") && StaticVariables.hasSwithed))
        {
            rb.velocity = new Vector2(dirX * StaticVariables.playerOneSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(dirX * StaticVariables.playerTwoSpeed, rb.velocity.y);
        }

        UpdateAnimationState();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        dirX = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
     {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
         if (IsGrounded())
         { 
             jumpSoundEffect.Play();
             if ((transform.gameObject.CompareTag("Player") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player2") && StaticVariables.hasSwithed))
             {
                 rb.velocity = new Vector2(rb.velocity.x, StaticVariables.playerOneJumpForce);
             }
             else
             {
                 rb.velocity = new Vector2(rb.velocity.x, StaticVariables.playerTwoJumpForce);

            }
        }
     }

    public void OnAttack(InputAction.CallbackContext context)
     {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        attackSoundEffect.Play();
        anim.SetTrigger("attack");
    }

    private void UpdateAnimationState()
    {
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
