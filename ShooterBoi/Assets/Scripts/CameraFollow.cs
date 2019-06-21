using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;

        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position - offset, 3f * Time.deltaTime);
    }
}
