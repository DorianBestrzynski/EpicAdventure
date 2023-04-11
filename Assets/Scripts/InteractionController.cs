using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] public Transform detectionPoint;

    private const float detectionRadius = 0.2f;

    [SerializeField] public LayerMask interactionLayer;


    void Update()
    {
        if (DetectObject())
        {
            Debug.Log("Collider");
            if (InteractInput())
            {
                Debug.Log("Interaction");
            }
        }
    }


    bool InteractInput()
    {
        if ((detectionPoint.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
        {
            return Input.GetKeyDown(KeyCode.RightShift);
        }
        else
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }

    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, interactionLayer);

    }
}
