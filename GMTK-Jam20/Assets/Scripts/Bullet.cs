using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    public RandomSound bulletSound;

    public bool isPlayerBullet = true;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        audioSource.volume = bulletSound.Volume;
        audioSource.pitch = bulletSound.Pitch;
        audioSource.Play();

        Destroy(gameObject, 5.0f);   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayerBullet)
        {
            EnemyHealth health = collision.transform.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else
        {
            PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.OnHit();
            }
        }

        Destroy(gameObject);
    }
}
