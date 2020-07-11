using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D rb;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !playerMovement.isDashing)
        {
            playerMovement.isDashing = true;
        }
    }
}
