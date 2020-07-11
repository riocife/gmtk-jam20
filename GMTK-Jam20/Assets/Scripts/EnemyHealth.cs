using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action onEnemyDied;

    // Hits needed to die
    public int hits = 2;

    public void TakeDamage(int damage)
    {
        hits -= damage;
        if (hits <= 0)
        {
            onEnemyDied.Invoke();
            Destroy(gameObject);
        }
    }
}
