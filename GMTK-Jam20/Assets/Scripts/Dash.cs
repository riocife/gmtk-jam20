using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 8f;
    public float dashDistance = 5f;
    public float dashCooldown = 3f;

    public LayerMask dashLayerMask;

    public CameraShakeParams dashShakeParams = new CameraShakeParams(30f, 20f, 1f, 2f);

//    public float dashShakeDuration = 0.15f;
//    public float dashShakeMagnitude = 0.4f;

    PlayerMovement playerMovement;
    Rigidbody2D rb;
    Camera mainCamera;
//    CameraShake cameraShake;

    Vector2 dashDir;
    Vector2 dashTarget;

    bool cooldown = false;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        mainCamera = Camera.main;
//        cameraShake = mainCamera.GetComponent<CameraShake>();
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
        CameraShaker.Instance.ShakeOnce(40f, 20f, 0.2f, 0.5f);
//        StartCoroutine(cameraShake.Shake(dashShakeDuration, dashShakeMagnitude));
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
        if (!playerMovement.isDashing && !cooldown) return;

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

        if (dashCooldown >= 0.01f)
        {
            cooldown = true;
            StartCoroutine(RegainDash());
        }
    }

    IEnumerator RegainDash()
    {
        yield return new WaitForSeconds(dashCooldown);
        cooldown = false;
    }
}
