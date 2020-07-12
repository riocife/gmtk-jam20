using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float delayBeforeChase = 1f;

    void Start()
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
