using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    PlayerMovement player;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.OnHit();
            Destroy(gameObject);
        }
    }
}
