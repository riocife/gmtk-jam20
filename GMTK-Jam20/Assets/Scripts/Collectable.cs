using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    public static int num = 0;
    public int diamond_num = 0;
    public static Action<int> onCollected;

    private void Awake()
    {
        num = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject == null) return;
        PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
        if (player != null)
        {
            num++;
            onCollected.Invoke(diamond_num);
            Debug.Log("collected collectable");
            Destroy(gameObject);
        }
    }
}
