using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Camera mainCamera;
    SpriteRenderer sprite;

    void Start()
    {
        mainCamera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Vector3 relativeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (relativeMousePos.normalized.y > 0)
        {
            sprite.sortingOrder = 9;
        }
        else
        {
            sprite.sortingOrder = 10;
        }

    }
}
