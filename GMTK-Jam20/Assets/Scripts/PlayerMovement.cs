using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public bool isDashing = false;

    public GameObject playerStartPrefab;

    public bool isLookingRight = false;

    [HideInInspector] public Transform playerStart;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
    }

    void Start()
    {
        mainCamera = Camera.main;
        playerStart = Instantiate(playerStartPrefab, transform.position, Quaternion.identity).transform;
    }

    void Update()
    {
        if (!isDashing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            animator.SetFloat("HorizontalMove", moveInput.x);
            animator.SetFloat("VerticalMove", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }

       // Check the mouse position to see if we should flip
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if ((isLookingRight && mousePos.x < transform.position.x) ||
            (!isLookingRight && mousePos.x > transform.position.x))
        {
            // Flip
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
            isLookingRight = !isLookingRight;
        }
    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }
    }
}
