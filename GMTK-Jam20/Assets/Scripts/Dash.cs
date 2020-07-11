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
            StartCoroutine(StartDash());
        }
    }

    IEnumerator StartDash()
    {
        playerMovement.isDashing = true;
        rb.AddForce(Vector2.zero, ForceMode2D.Impulse);

        // Dash damage could be checking a collider while the "is dashing"
        // TODO also add animation

        yield return new WaitForSeconds(2.0f);

        // Stop animation

        playerMovement.isDashing = false;
    }
}
