using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashTime = 0.5f;

    PlayerMovement playerMovement;
    Rigidbody2D rb;
    Camera mainCamera;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        mainCamera = Camera.main;    
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

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDir = ((Vector2)(mousePos - transform.position)).normalized;

        Debug.Log("DASH!");
        rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);

        // Dash damage could be checking a collider while the "is dashing"
        // TODO also add animation

        yield return new WaitForSeconds(dashTime);

        // Stop animation

        playerMovement.isDashing = false;
    }
}
