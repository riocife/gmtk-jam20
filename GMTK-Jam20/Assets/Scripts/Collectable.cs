using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    public static int num = 0;
    public static Action onCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
        if (player != null)
        {
            num++;
            onCollected.Invoke();

            Destroy(gameObject);
        }
    }
}
