using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action onEnemyDied;

    AudioSource audioSource;

    // Hits needed to die
    public int hits = 1;

    public AudioClip deathClip;

    public GameObject deathVFXPrefab;

    void Awake()
    {
        PlayerHealth.onPlayerDied += OnPlayerDied;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Could not find audio source!");
        }
    }

    public void TakeDamage(int damage)
    {
        hits -= damage;
        if (hits <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onEnemyDied?.Invoke();

        // Stop current clip and change it to explosion
        audioSource.Stop();
        audioSource.clip = deathClip;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.Play();

        // Spawn vfx
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

        DisableAI();
    }

    void DisableAI()
    {
        Collider2D[] colls = GetComponents<Collider2D>();
        foreach (Collider2D coll in colls)
        {
            coll.enabled = false;
        }

        EnemyAttack enemyAttack = GetComponent<EnemyAttack>();
        if (enemyAttack)
        {
            enemyAttack.enabled = false;
        }

        Seeker seeker = GetComponent<Seeker>();
        if (seeker)
        {
            seeker.enabled = false;
        }

        AIPath path = GetComponent<AIPath>();
        if (path)
        {
            path.enabled = false;
        }

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        PlayerHealth.onPlayerDied -= OnPlayerDied;

        Destroy(gameObject, 0.5f);
    }

    void OnPlayerDied()
    {
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<Seeker>().enabled = false;
        GetComponent<AIPath>().enabled = false;
    }
}
