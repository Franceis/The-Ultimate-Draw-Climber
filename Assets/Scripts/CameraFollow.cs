using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// A simple script that make the Main Camera follow the player
    /// </summary>
    private Vector3 offSet;

    private Transform playerTransform;

    void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform; //Get the player transform
    }

    void Update()
    {
        //Make the magic
        offSet = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z * 14.0f);
        transform.position = offSet;
    }
}
