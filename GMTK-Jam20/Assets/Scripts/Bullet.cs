using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 5.0f);   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth health = collision.transform.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
