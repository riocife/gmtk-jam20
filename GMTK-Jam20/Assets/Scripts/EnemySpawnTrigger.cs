using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public bool onlyTriggerOnce = true;
    
    List<EnemySpawner> spawners = new List<EnemySpawner>();

    public void RegisterSpawner(EnemySpawner spawner)
    {
        spawners.Add(spawner);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SpawnEnemy();
            }

            if (onlyTriggerOnce)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(2f, 2f, 2f));
    }
}
