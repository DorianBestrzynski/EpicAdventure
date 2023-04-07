using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2")) && !transform.gameObject.CompareTag("CloudPlatform"))
        {
    
            Debug.Log("Should not be here");
            collision.gameObject.transform.SetParent(transform);
        }
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.transform.SetParent(null);
        }
    }

  
}

