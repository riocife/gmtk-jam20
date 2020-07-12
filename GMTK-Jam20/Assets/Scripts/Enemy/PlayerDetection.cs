using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    AIDestinationSetter ai;
    bool foundPlayer = false;

    AudioSource audioSource;

    public Action onEnemyEnter;
    public Action onEnemyLeave;

    private void Awake()
    {
        ai = transform.GetComponentInParent<AIDestinationSetter>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && !foundPlayer)
        {
            foundPlayer = true;
            if (ai)
            {
                ai.target = player.transform;
            }

            onEnemyEnter.Invoke();

            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            onEnemyLeave.Invoke();
        }
    }
}
