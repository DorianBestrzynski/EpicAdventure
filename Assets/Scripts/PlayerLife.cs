using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rb;

    [SerializeField] private int magicianLifes = 2;

    [SerializeField] private int knightLifes = 4;

    [SerializeField] private Text playerOneLife;

    [SerializeField] private Text playerTwoLife;

    [SerializeField] private AudioSource deathSound;

    [SerializeField] private Transform player;

    [SerializeField] private AudioSource collectionSound;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (StaticVariables.isPLayerOneMagician)
        {
            StaticVariables.playerOneLife = magicianLifes;
        }
        else
        {
            StaticVariables.playerOneLife = knightLifes;
        }

        if (StaticVariables.isPLayerTwoMagician)
        {
            StaticVariables.playerTwoLife = magicianLifes;
        }
        else
        {
            StaticVariables.playerTwoLife = knightLifes;
        }

        playerOneLife.color = Color.white;
        playerTwoLife.color = Color.white;
        playerOneLife.text = "P1 Life:" + StaticVariables.playerOneLife;
        playerTwoLife.text = "P2 Life:" + StaticVariables.playerTwoLife;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bullet"))
        {
            DieSound();
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                Debug.Log("Trigger health P2" + StaticVariables.isEasyMode);
                if (StaticVariables.isEasyMode)
                {
                    StaticVariables.playerTwoLife--;
                }
                else
                {
                    StaticVariables.playerTwoLife -= 2;
                }


                if (StaticVariables.playerTwoLife < 1)
                {
                    playerTwoLife.text = "P2 Life:" + StaticVariables.playerTwoLife;
                    Die();
                }
                else
                {
                    playerTwoLife.text = $"P2 Life:" + StaticVariables.playerTwoLife;
                }
            }
            else 
            {
                Debug.Log("Trigger health P1 " + StaticVariables.isEasyMode);
                if (StaticVariables.isEasyMode)
                {
                    StaticVariables.playerOneLife--;
                }
                else
                {
                    StaticVariables.playerOneLife -= 2;
                }
                if (StaticVariables.playerOneLife < 1)
                {
                    playerOneLife.text = $"P1 Life:" + StaticVariables.playerOneLife;
                    Die();
                }
                else
                {
                    playerOneLife.text = $"P1 Life:" + StaticVariables.playerOneLife;
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("HealthPotion"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                if (StaticVariables.playerTwoLife < 8)
                {
                    if (StaticVariables.isEasyMode)
                    {
                        StaticVariables.playerTwoLife+=2;
                    }
                    else
                    {
                        StaticVariables.playerTwoLife ++;
                    }

                    playerTwoLife.text = $"P2 Life:" + StaticVariables.playerTwoLife;
                }
            }
            else
                {
                    if (StaticVariables.playerOneLife < 8)
                    {
                    if (StaticVariables.isEasyMode)
                    {
                        StaticVariables.playerOneLife += 2;
                    }
                    else
                    {
                        StaticVariables.playerOneLife++;
                    }
                    playerOneLife.text = $"P1 Life:" + StaticVariables.playerOneLife;
                    }
                }
            collectionSound.Play();
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("SpeedPotion"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                StaticVariables.playerTwoSpeed += 5f;

            }
            else
            {
                StaticVariables.playerOneSpeed += 5f;
            }
            collectionSound.Play();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("OutOfMap"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                playerOneLife.text = $"P2 Life:" + 0;
            }
            else
            {
                playerOneLife.text = $"P1 Life:" + 0;
            }
            deathSound.Play();
            Die();
            Destroy(collision.gameObject);
        }


    }
    


    private void Die()
    {
        StaticVariables.playerOneCollectibles = StaticVariables.playerOneFinalCollectibles;
        StaticVariables.playerTwoCollectibles = StaticVariables.playerTwoFinalCollectibles;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        RestartLevel();
    }

    private void DieSound()
    {
        deathSound.Play();
    }

    private void RestartLevel()
    {
        StaticVariables.isDead = false;
        Debug.Log("Should stop animation");
        Debug.Log(StaticVariables.isDead);
        StartCoroutine(Waiter());
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
