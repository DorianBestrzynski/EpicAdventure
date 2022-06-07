using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float dieTime, damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());

    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D col)
    {
   
            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Terrain"))
            {
                if(col.gameObject.CompareTag("Player"))
                {
                    StaticVariables.isDead = true;
                }

            Die();


                
            
        }

    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    private void Die()
    {
        Debug.Log("Now");

        Destroy(gameObject);
    }
}
