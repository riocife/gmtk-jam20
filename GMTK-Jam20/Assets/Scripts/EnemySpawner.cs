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

    GameObject enemy;
    
    void Start()
    {
        if (!spawnTrigger)
        {
            SpawnEnemy();
        }
        else
        {
            spawnTrigger.RegisterSpawner(this);
        }
    }

    public void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().onEnemyDied += OnEnemyDied;
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
}
