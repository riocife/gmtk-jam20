using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action onEnemyDied;

    public AudioClip deathClip;
    AudioSource audioSource;

    // Hits needed to die
    public int hits = 1;

    void Awake()
    {
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
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        onEnemyDied?.Invoke();

        audioSource.Play();

        // Stop current clip and change it to explosion
        //audioSource.Stop();
        //audioSource.clip = deathClip;
        //audioSource.loop = false;
        //audioSource.volume = 1f;
        //        audioSource.Play();

//        yield return new WaitForSeconds(audioSource.clip.length + 0.1f);

        gameObject.SetActive(false);

        Destroy(gameObject, 0.5f);

        yield return null;
    }
}
