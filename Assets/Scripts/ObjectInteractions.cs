using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;


    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KnightInteract"))
        {
            Debug.Log("Collision");
            if (player.gameObject.name != "Magician")
            {
                Debug.Log("Ddd");
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;

            }
        }
    }
}
