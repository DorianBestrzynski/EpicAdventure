using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;
    [SerializeField] private Camera camera;

    private float maxDistance = 8;

    private void Update()
    {
       
        float dist = Math.Abs(Vector3.Distance(player.position, player2.position));

        transform.position = new Vector3((player.position.x + player2.position.x) / 2,
            (player.position.y + player2.position.y) / 2, transform.position.z);
         if (dist < 2 * maxDistance)
        {

            camera.orthographicSize = 10f;
        }

        else if(dist < 60f)
        {
            camera.orthographicSize = 20f;

        }

        else 
        { 
            camera.orthographicSize = 40f;
        }


    }
}
