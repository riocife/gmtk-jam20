using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    AIDestinationSetter ai;

    bool foundPlayer = false;

    private void Awake()
    {
        ai = transform.GetComponentInParent<AIDestinationSetter>();   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && !foundPlayer)
        {
            foundPlayer = true;
            ai.target = player.transform;
        }
    }
}
