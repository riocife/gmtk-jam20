using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    PlayerDetection playerDetection;
    PlayerMovement player;

    bool foundPlayer = false;
    bool shotCooldown = false;

    public float awakeDelay = 1f;
    public float bulletForce = 20f;
    public float fireRate = 1f;
    public LayerMask blockingShotLayerMask;

    public GameObject bulletPrefab;

    void Awake()
    {
        playerDetection = GetComponentInChildren<PlayerDetection>();
        if (playerDetection)
        {
            playerDetection.onEnemyEnter += OnEnemyEnter;
            playerDetection.onEnemyLeave += OnEnemyLeave;
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if (awakeDelay >= 0.01f)
        {
            StartCoroutine(AwakeDelay());
        }
    }

    IEnumerator AwakeDelay()
    {
        shotCooldown = true;
        yield return new WaitForSeconds(awakeDelay);
        shotCooldown = false;
    }

    void Update()
    {
        if (player != null && foundPlayer && !shotCooldown)
        {
            Debug.Log("SHOOT!");
            Shoot();
        }
    }

    void Shoot()
    {
        // Can only shoot if there is a straight line towards player
        Vector2 toPlayer = player.transform.position - transform.position;
        Vector2 direction = toPlayer.normalized;

        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, toPlayer.magnitude + 5f, blockingShotLayerMask);
        if (hit)
        {
            PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
            if (!player) return;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        shotCooldown = true;
        yield return new WaitForSeconds(fireRate);
        shotCooldown = false;
    }

    void OnEnemyEnter()
    {
        foundPlayer = true;
    }

    void OnEnemyLeave()
    {
        foundPlayer = false;
    }


}
