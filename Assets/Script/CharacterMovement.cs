using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runningSpeed = 5f;
    public float walkSpeed = 0.3f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public float turnSpeed = 120f;
    private float smoothTime = 0.02f;

    [Header("Lives Settings")]
    public int maxLives = 5;
    private int currentLives;
    // public bool isFalling;

    [Header("UI Elements")]
    public TextMeshProUGUI livesText;
    public float fallForce = 5f;
    private bool isSprinting = false;
    private float walkSpeedRatioNormalized = 0.5f;
    private PlayerHealth playerHealth;

    [Header("Ground Check Settings")]
    CharacterController characterController;
    Animator animator;
    Vector3 velocity;
    private float turnSmoothVelocity;
    private bool jumpRequested = false;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        currentLives = maxLives;
        // isFalling = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequested = true;
            
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))&& !GameManager.isGameOver)
        {
            PauseManager pauseManager = FindObjectOfType<PauseManager>();
            if (pauseManager != null)
                pauseManager.TogglePause();
        }
    }
    bool IsGroundedRaycast()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f; // Diatas kaki
        return Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);
    }

    void FixedUpdate()
    {
        // read input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (playerHealth != null && playerHealth.isGameOver)
        {
            if (animator != null) animator.SetFloat("speed", 0f);
            return;
        }

        bool grounded = IsGroundedRaycast();

        // rotation + horizontal movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            float finalSpeed = isSprinting ? runningSpeed : walkSpeed;
            
            // lower speed when airborne
            if (!grounded)
                finalSpeed *= 0.8f;
            
            characterController.Move(moveDir.normalized * finalSpeed * Time.deltaTime);
        }

        // grounded handling
        if (grounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        // handle jump
        if (jumpRequested && grounded)
        {
            jumpRequested = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (animator != null) animator.SetTrigger("Jump");
        }
        else if (jumpRequested && !grounded)
        {
            jumpRequested = false;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // compute normalized motion
        float moveSpeedMotionNormalized = Mathf.Clamp01(new Vector2(moveX, moveZ).magnitude);
        if (!isSprinting && moveSpeedMotionNormalized > walkSpeedRatioNormalized)
            moveSpeedMotionNormalized = walkSpeedRatioNormalized;

        // update animator
        if (animator != null)
        {
            animator.SetFloat("speed", Mathf.Abs(moveSpeedMotionNormalized));
            animator.SetBool("IsGrounded", grounded);
            animator.SetFloat("VerticalVel", velocity.y);
        }
    }
}