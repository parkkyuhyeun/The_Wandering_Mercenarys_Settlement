using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void FixedUpdate()
    {
        var camZ = transform.position.z;
        var playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, camZ);
    }
}
