using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [HideInInspector] public bool mustTurn = false;

    [SerializeField]private Rigidbody2D rb;

    [SerializeField] private float walkSpeed, followRange, shootRange, timeBtwShots, shootSpeed;

    private bool canShoot;
    private bool isFlipped = false;

    private float distanceToPlayer;
    private float distanceToPlayer2;

    [SerializeField] private Transform groundCheckPos;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject bullet;

    private Animator anim;

    [SerializeField] private Collider2D bodyColider;

    [SerializeField] private Transform player, secondPlayer, shootPos, cavePos;

    private Transform closestPlayer;

    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        anim = GetComponent<Animator>();
        closestPlayer = player;
        anim.SetBool("isMoving", true);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(StaticVariables.bossLife);
        if (StaticVariables.bossLife <= 0) 
        {
            StartCoroutine(Die());
        } else 
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            distanceToPlayer2 = Vector2.Distance(transform.position, secondPlayer.position);

            closestPlayer = distanceToPlayer < distanceToPlayer2 ? player : secondPlayer;

            if (canShoot && Math.Min(distanceToPlayer, distanceToPlayer2) <= shootRange)
            {
            // Debug.Log("Follow action");
               ShootingAction(closestPlayer);
            }
            else if (Math.Min(distanceToPlayer, distanceToPlayer2) <= followRange)
            {
                // Debug.Log("Follow action");
                anim.SetBool("isMoving", true);
                LookAtPlayer(closestPlayer);
                FollowPlayer(closestPlayer);
            }
            else 
            {
                Hide();
            }
        }

       
    }

    private void ShootingAction(Transform player)
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("isMoving", false);
        if (canShoot && !StaticVariables.isDead)
        {
            StartCoroutine(Attack());
        }
    }

    
    public void LookAtPlayer (Transform closestPlayer) {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > closestPlayer.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false ;
        }
        else if (transform.position.x < closestPlayer.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
   

    IEnumerator Attack()
    {
        canShoot = false;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * isPositive(transform.localScale.x) * -1, newBullet.transform.localScale.y);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * 20 * isPositive(newBullet.transform.localScale.x) * Time.deltaTime, 0.1f);
        anim.SetBool("isMoving", false);
        yield return new WaitForSeconds(timeBtwShots);
        anim.SetBool("isMoving", true);
        canShoot = true;
    }

     IEnumerator Die()
    {
        anim.SetTrigger("death");
        yield return new WaitForSeconds(1.5f);

        if (StaticVariables.playerOneCollectibles > StaticVariables.playerTwoCollectibles)
        {
            SceneManager.LoadScene("Custscene4");
        } 
        else
        { 
            SceneManager.LoadScene("Custscene5");
        }
    }

    private void FollowPlayer(Transform targetPlayer)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, walkSpeed * Time.deltaTime);
    }

    private void Hide()
    {

        transform.position = Vector2.MoveTowards(transform.position, cavePos.position, walkSpeed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, cavePos.position);
        if (dist < 3f)
        {
            if (StaticVariables.bossLife < 10)
            {
                StaticVariables.bossLife += 5;
                healthBar.value = StaticVariables.bossLife;
            }
            else
            {
                StaticVariables.bossLife = 15;
                healthBar.value = StaticVariables.bossLife;
            }
        }
    }

    private int isPositive(float num)
    {
        return num > 0 ? 1 : -1;
    }
}
