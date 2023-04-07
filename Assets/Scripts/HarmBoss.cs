using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarmBoss : MonoBehaviour
{
    public int attackDamageMagician = 1;
    public int attackDamageKnight = 3;
    public float attackRange = 5f;
    public LayerMask attackMask;
    private int playerCollectibles;
    
    public Animator bossAnimator;
    public Slider healthBar;

    private void Start()
    {
        healthBar.maxValue = StaticVariables.bossLife;
       if (gameObject.CompareTag("Player")) 
        {
            playerCollectibles = StaticVariables.playerOneCollectibles;
        } 
        else 
        {
            playerCollectibles = StaticVariables.playerTwoCollectibles;
        }
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask) ;
        if (colInfo != null)
        {
            StartCoroutine(Hurt());
            if (transform.gameObject.name == "Magician")
            {

                StaticVariables.bossLife -= attackDamageMagician;
            }
            else
            {
                StaticVariables.bossLife -= attackDamageKnight;
            }

                healthBar.value = StaticVariables.bossLife;

            if (gameObject.CompareTag("Player")) 
            {
                StaticVariables.playerOneCollectibles = playerCollectibles + 1;
            } 
            else 
            {
                StaticVariables.playerTwoCollectibles = playerCollectibles + 1;
            }
        }
    }

     IEnumerator Hurt()
    {
        bossAnimator.SetTrigger("hurt");
        yield return new WaitForSeconds(0.3f);

    }
}
