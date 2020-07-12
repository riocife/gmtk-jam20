using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // After the enemy dies, how long to respawn. Set this to negative to avoid respawn.
    public float reSpawnDelay = 10f;

    // If this is set, spawn only occurs with trigger.
    public EnemySpawnTrigger spawnTrigger;

    public GameObject enemyPrefab;

    bool canSpawn = true;

    GameObject enemy;
    AudioSource audioSource;

    AudioListener audioListener;

    bool isPlayerDead = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerHealth.onPlayerDied += OnPlayerDied;
    }

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        audioListener = Camera.main.GetComponent<AudioListener>();

        if (!spawnTrigger)
        {
            SpawnEnemy(true);
        }
        else
        {
            spawnTrigger.RegisterSpawner(this);
        }
    }

    public void SpawnEnemy(bool firstSpawn = false)
    {
        if (isPlayerDead) return;
        if (!canSpawn) return;

        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().onEnemyDied += OnEnemyDied;

        if (!firstSpawn)
        {
            audioSource.Play();
        }
    }

    void OnEnemyDied()
    {
        enemy.GetComponent<EnemyHealth>().onEnemyDied -= OnEnemyDied;
        if (reSpawnDelay > 0f)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(reSpawnDelay);
        SpawnEnemy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    void OnPlayerDied()
    {
        isPlayerDead = true;
    }
}
