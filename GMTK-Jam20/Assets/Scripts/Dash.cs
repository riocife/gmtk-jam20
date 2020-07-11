using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashTime = 0.5f;
    public float dashSpeed = 8f;
    public float dashDistance = 5f;

    public LayerMask dashLayerMask;

    PlayerMovement playerMovement;
    Rigidbody2D rb;
    Camera mainCamera;

    Vector2 dashDir;
    Vector2 dashTarget;

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
            StartDash();
        }
    }

    void FixedUpdate()
    {
        PerformDash();    
    }

    void StartDash()
    {
        playerMovement.isDashing = true;
//        mainCamera.GetComponent<Animator>().SetBool("dashing", true);

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dashDir = ((Vector2)mousePos - rb.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDir, dashDistance, dashLayerMask);
        if (hit)
        {
            dashTarget = hit.point;
        }
        else
        {
            dashTarget = rb.position + dashDir * dashDistance;
        }
    }

    void PerformDash()
    {
        if (!playerMovement.isDashing) return;

        Vector2 target = Vector2.MoveTowards(rb.position, dashTarget, dashSpeed * Time.fixedDeltaTime);
        rb.MovePosition(target);

        if (Vector2.Distance(rb.position, dashTarget) < 1f)
        {
            StopDash();
        }
    }

    void StopDash()
    {
        playerMovement.isDashing = false;
//        mainCamera.GetComponent<Animator>().SetBool("dashing", false);
    }
}
