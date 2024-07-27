using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public float xOffset, height, distance;
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x + xOffset, player.position.y + height, distance);
    }
}
