using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{

    // [SerializeField] private Transform player;
    private Rigidbody2D rb;
    [SerializeField] private GameObject crateInfo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Collision");
            if (collision.gameObject.name != "Magician")
            {
                if(crateInfo != null)
                {
                    crateInfo.SetActive(false);
                }
                Debug.Log("Ddd");
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    private void OnCollisionExit2S(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            if (collision.gameObject.name != "Magician")
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
