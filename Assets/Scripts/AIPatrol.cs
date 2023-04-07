using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{

    [HideInInspector] public bool mustPatrol;

    [SerializeField]private Rigidbody2D rb;

    [SerializeField] private float walkSpeed, range, timeBtwShots, shootSpeed;

    private bool mustTurn, canShoot;

    private float distanceToPlayer;
    private float distanceToPlayer2;

    [SerializeField] private Transform groundCheckPos;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject bullet;

    private enum MovementState { Idle, Running};

    private Animator anim;




    [SerializeField] private Collider2D bodyColider;

    [SerializeField] private Transform player, secondPlayer, shootPos;
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementState state;
        if (mustPatrol)
        {
            Patrol();
        }

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        distanceToPlayer2 = Vector2.Distance(transform.position, secondPlayer.position);

        if (distanceToPlayer <= range)

        {
            Debug.Log("P1 " + canShoot);
           state = ShootingAction(player);
        }
        else if (distanceToPlayer2 <= range)
        {
            Debug.Log("P2 " + canShoot);
            state = ShootingAction(secondPlayer);
        }

        else
        {
            state = MovementState.Running;
            mustPatrol = true;
        }
        anim.SetInteger("state", (int)state);
    }

    private MovementState ShootingAction(Transform player)
    {
        if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
        MovementState state = MovementState.Idle;
        mustPatrol = false;
        rb.velocity = Vector2.zero;
        if (canShoot && !StaticVariables.isDead)
        {
            StartCoroutine(Attack());
        }

        return state;
    }
   

     IEnumerator Attack()
     {
         canShoot = false;
         yield return new WaitForSeconds(timeBtwShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        Debug.Log("new bullet");

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.deltaTime, 0.1f);
        Debug.Log("Here");
        canShoot = true;

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
