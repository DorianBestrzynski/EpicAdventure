using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CloudPlatform : MonoBehaviour
{
    [SerializeField] public BoxCollider2D triggerColider;

    [SerializeField] public BoxCollider2D areaCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        triggerColider.enabled = true;
        areaCollider.enabled = true;
        if (transform.gameObject.CompareTag("CloudPlatform") && collision.gameObject.name == "Magician")
        {

            Debug.Log("Here");
            collision.gameObject.transform.SetParent(transform);
        }
        else
        {
            Debug.Log("Turn off");
            triggerColider.enabled = false;
            areaCollider.enabled = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Magician" ) 
        {
            Debug.Log("NA");
            collision.gameObject.transform.SetParent(null);
        }
        else
        {
            Debug.Log("Turn oon");
            triggerColider.enabled = true;
            areaCollider.enabled = true;
        }
    }


}

