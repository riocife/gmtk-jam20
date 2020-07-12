using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float delayBeforeChase = 1f;

    Transform playerDetection;

    public Collider2D triggerCollider;

    void Start()
    {
        if (delayBeforeChase >= 0.01f)
        {
            playerDetection = transform.GetChild(0);
            playerDetection.gameObject.SetActive(false);
            StartCoroutine(ActivateDetection());

            triggerCollider.enabled = false;
        }
    }

    IEnumerator ActivateDetection()
    {
        yield return new WaitForSeconds(delayBeforeChase);
        playerDetection.gameObject.SetActive(true);

        triggerCollider.enabled = true;
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
