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
       Debug.Log("Attack method");

        Vector3 pos = transform.position;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask) ;
        if (colInfo != null)
        {
            StartCoroutine(Hurt());
            if (transform.gameObject.name == "Magician")
            {
                Debug.Log("taken life from boss");
                StaticVariables.bossLife -= attackDamageMagician;
                Debug.Log("Boos life: " + StaticVariables.bossLife);
            }
            else
            {
                Debug.Log("taken life from boss");

                StaticVariables.bossLife -= attackDamageKnight;
                Debug.Log("Boos life: " + StaticVariables.bossLife);

            }

                healthBar.value = StaticVariables.bossLife;

            if (gameObject.CompareTag("Player")) 
            {
                Debug.Log("taken life from boss");
                StaticVariables.playerOneCollectibles = playerCollectibles + 1;
            } 
            else 
            {
                Debug.Log("taken life from boss");
                StaticVariables.playerTwoCollectibles = playerCollectibles + 1;
            }
        }
    }

     IEnumerator Hurt()
    {
        Debug.Log("Setting trigger to hurt");
        bossAnimator.SetTrigger("hurt");
        yield return new WaitForSeconds(0.3f);

    }
}
