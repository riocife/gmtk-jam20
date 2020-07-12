using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    bool isMoving = false;
    float originalSpeed;

    Vector3 relativeMousePos = new Vector3();

    SpriteRenderer sprite;

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
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
//        animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
//        sprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        mainCamera = Camera.main;
        playerStart = Instantiate(playerStartPrefab, transform.position, Quaternion.identity).transform;
        originalSpeed = moveSpeed;
    }

    void Update()
    {
//        animator.SetBool("isDashing", isDashing);
        if (!isDashing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
           
            animator.SetFloat("Speed", moveInput.x + moveInput.y);

            relativeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                        
            animator.SetFloat("Vertical", relativeMousePos.normalized.y);
            
            // Check if its moving
            if (moveInput.x != 0.0f || moveInput.y != 0.0f)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            sprite.flipX = (relativeMousePos.normalized.y > .0f);

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
            if (moveInput.magnitude > 1)
            {
                moveInput = moveInput.normalized;
            }
            Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }
    }
}
