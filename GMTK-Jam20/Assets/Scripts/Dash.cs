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

    public float dashShakeMag = 2f;
    public float dashShakeTime = 0.75f;

    public RandomSound dashSound;

    PlayerMovement playerMovement;
    Rigidbody2D rb;
    AudioSource audioSource;
    Camera mainCamera;
    Animator animator;

    Vector2 dashDir;
    Vector2 dashTarget;

    bool cooldown = false;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

        audioSource.volume = dashSound.Volume;
        audioSource.pitch = dashSound.Pitch;
        audioSource.Play();

        // Camera shake
        mainCamera.GetComponent<CameraMove>().Shake((Vector3)dashDir, dashShakeMag, dashShakeTime);

        // Animation

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

    public void StopDash()
    {
        playerMovement.isDashing = false;

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
