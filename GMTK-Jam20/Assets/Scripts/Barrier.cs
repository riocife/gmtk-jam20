using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] int collectablesNeeded = 2;

    void Start()
    {
        Collectable.onCollected += OnCollected;
    }

    void OnCollected(int num)
    {
        if (Collectable.num >= collectablesNeeded)
        {
            // Unregister delegate
            Collectable.onCollected -= OnCollected;

            Destroy(gameObject);
        }
    }
}
