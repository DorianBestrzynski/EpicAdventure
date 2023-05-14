using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform player2;
    [SerializeField] private Camera camera;
    // [SerializeField] private GameObject player;
    // [SerializeField] private GameObject player2;

    private float maxDistance = 8;
    private float minSize = 10f;
    private float maxSize = 40f;
    public float smoothTime = 0.2f;  

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
    // private void Update()
    // {
    //     Vector3 distanceVector = player.position - player2.position;
    //     float distance = distanceVector.magnitude;
    //             Debug.Log(distance);


    //     float desiredOrthoSize = Mathf.Lerp(minSize, maxSize, distance / maxDistance);
    //     Debug.Log(desiredOrthoSize);

    //     camera.orthographicSize = desiredOrthoSize;
    // }
}
