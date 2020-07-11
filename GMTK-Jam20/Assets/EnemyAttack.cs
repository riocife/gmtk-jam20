using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
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
