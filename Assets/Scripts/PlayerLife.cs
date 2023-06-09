using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rb;

    [SerializeField] private int magicianLives = 2;

    [SerializeField] private int knightLives = 4;

    [SerializeField] private Text playerOneLife;

    [SerializeField] private Text playerTwoLife;

    [SerializeField] private AudioSource deathSound;

    [SerializeField] private Transform player;

    [SerializeField] private AudioSource collectionSound;

     [SerializeField] private AudioSource potionSound;

    private bool dying;
    // Start is called before the first frame update
    private void Start()
    {
        AudioListener.volume = 0.3f;
        dying = false;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (StaticVariables.isPLayerOneMagician)
        {
            if(StaticVariables.playerOneLife < 1 || StaticVariables.hasRestartedLevel) { 
            StaticVariables.playerOneLife = magicianLives;
                }
        }
        else
        {
            if(StaticVariables.playerOneLife < 1 || StaticVariables.hasRestartedLevel) { 
            StaticVariables.playerOneLife = knightLives;
                }
        }

        if (StaticVariables.isPLayerTwoMagician)
        {
            if(StaticVariables.playerTwoLife < 1 || StaticVariables.hasRestartedLevel) { 
            StaticVariables.playerTwoLife = magicianLives;
                }
        }
        else
        {
            if(StaticVariables.playerTwoLife < 1 || StaticVariables.hasRestartedLevel) { 
            StaticVariables.playerTwoLife = knightLives;
                }
        }

        if (StaticVariables.hasRestartedLevel)
        {
            StaticVariables.hasRestartedLevel = false;
        }

        playerOneLife.color = Color.white;
        playerTwoLife.color = Color.white;
        playerOneLife.text = "P1 Życia:" + StaticVariables.playerOneLife;
        playerTwoLife.text = "P2 Życia:" + StaticVariables.playerTwoLife;
      
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
                    if(StaticVariables.playerTwoLife < 0)
                    {
                        StaticVariables.playerTwoLife = 0;
                    }
                    playerTwoLife.text = "P2 Życia:" + StaticVariables.playerTwoLife;
                    StaticVariables.playerTwoDeathsOnLevel++;

                    Die();
                }
                else
                {
                   if(StaticVariables.playerTwoLife < 0)
                    {
                        StaticVariables.playerTwoLife = 0;
                    }
                    playerTwoLife.text = $"P2 Życia:" + StaticVariables.playerTwoLife;
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
                  if(StaticVariables.playerOneLife < 0)
                    {
                        StaticVariables.playerOneLife = 0;
                    }
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                    StaticVariables.playerOneDeathsOnLevel++;
 
                    Die();
                }
                else
                {
                    if(StaticVariables.playerOneLife < 0)
                    {
                        StaticVariables.playerOneLife = 0;
                    }
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("HealthPotion"))
        {
            Debug.Log("heeeerere ");
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

                    playerTwoLife.text = $"P2 Życia:" + StaticVariables.playerTwoLife;
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
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                    }
                }
            potionSound.Play();
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("SpeedPotion"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                if(StaticVariables.isPLayerOneMagician)
                {
                    potionSound.Play();
                    Destroy(collision.gameObject);
                    StaticVariables.playerTwoSpeed += 5f;
                }
            }
            else
            {
                if(StaticVariables.isPLayerTwoMagician)
                {
                    potionSound.Play();
                    Destroy(collision.gameObject);
                    StaticVariables.playerOneSpeed += 5f;
                }
            }
        }

        else if (collision.gameObject.CompareTag("OutOfMap"))
        {

            Debug.Log("Out of map lol");
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                playerTwoLife.text = $"P2 Życia:" + 0;
                StaticVariables.playerTwoDeathsOnLevel++;
            }
            else
            {
                playerOneLife.text = $"P1 Życia:" + 0;
                StaticVariables.playerOneDeathsOnLevel++;
            }
            deathSound.Play();

            Die();
            Destroy(collision.gameObject);
        }
         else if (collision.gameObject.CompareTag("JumpPotion"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                if(StaticVariables.isPLayerOneMagician)
                {
                    potionSound.Play();
                    Destroy(collision.gameObject);
                    StaticVariables.playerTwoJumpForce += 2f;
                }
            }
            else
            {
                if(StaticVariables.isPLayerTwoMagician)
                {
                    potionSound.Play();
                    Destroy(collision.gameObject);
                    StaticVariables.playerOneJumpForce += 2f;
                }
            }
        }

          else if (collision.gameObject.CompareTag("PoisonPotion"))
        {
            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                if(player.gameObject.name != "Magician") { 

                StaticVariables.playerTwoLife--;
                }
                else
                {
                StaticVariables.playerTwoLife++;

                }

                 if (StaticVariables.playerTwoLife < 1)
                {
                    playerTwoLife.text = "P2 Życia:" + StaticVariables.playerTwoLife;
                    Die();
                }
                else
                {
                    playerTwoLife.text = $"P2 Życia:" + StaticVariables.playerTwoLife;
                }
            }
            else
            {
                if(player.gameObject.name != "Magician") { 

                StaticVariables.playerOneLife--;
                }
                 else
                {
                StaticVariables.playerOneLife++;

                }

                if (StaticVariables.playerOneLife < 1)
                {
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                    Die();
                }
                else
                {
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                }
            }
            potionSound.Play();
            Destroy(collision.gameObject);
        }
    }

     private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTrap"))
        {
                  if (!dying) { 
            if(StaticVariables.playerTwoLife == 0 || StaticVariables.playerOneLife == 0)
            {

            }
            else { 
            DieSound();
                }

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
                    if(StaticVariables.playerTwoLife < 0)
                    {
                        StaticVariables.playerTwoLife = 0;
                    }
                    playerTwoLife.text = "P2 Życia:" + StaticVariables.playerTwoLife;
                    dying =true;
                    StaticVariables.playerTwoDeathsOnLevel++;
                    Die();
                }
                else
                {
                   if(StaticVariables.playerTwoLife < 0)
                    {
                        StaticVariables.playerTwoLife = 0;
                    }
                    playerTwoLife.text = $"P2 Życia:" + StaticVariables.playerTwoLife;
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
                  if(StaticVariables.playerOneLife < 0)
                    {
                        StaticVariables.playerOneLife = 0;
                    }
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                    dying =true;

                    Die();
                }
                else
                {
                    if(StaticVariables.playerOneLife < 0)
                    {
                        StaticVariables.playerOneLife = 0;
                    }
                    playerOneLife.text = $"P1 Życia:" + StaticVariables.playerOneLife;
                }
            }
            }
        }
    }
    


    private void Die()
    {
        StaticVariables.playerOneCollectibles = StaticVariables.playerOneFinalCollectibles;
        StaticVariables.playerTwoCollectibles = StaticVariables.playerTwoFinalCollectibles;
        StaticVariables.hasRestartedLevel = true;
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
        StartCoroutine(Waiter());
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        if((StaticVariables.playerOneDeathsOnLevel > 5 || StaticVariables.playerTwoDeathsOnLevel > 5) && !StaticVariables.isEasyMode)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
        else { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

    }
}
