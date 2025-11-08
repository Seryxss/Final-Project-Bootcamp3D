using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float runningSpeed = 10f;
    public float walkSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    public float turnSpeed = 120f;
    private float smoothTime = 0.02f;

    public int maxLives = 5;
    private int currentLives;
    public bool isFalling;

    public TextMeshProUGUI livesText;  
    public float fallForce = 5f;

    CharacterController characterController;
    Animator animator;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentLives = maxLives;
        isFalling = false;


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Danger"))
        {
            if (isFalling == false)
            {
                isFalling = true;
                TakeDamage(hit);
                velocity.x -= gravity * Time.deltaTime;
                characterController.Move(velocity * Time.deltaTime);
                animator.SetTrigger("falling");
                animator.SetFloat("speed", 0);
                isFalling = false;
            }
        }
    }

    void TakeDamage(ControllerColliderHit hit)
    {
        currentLives--;
        Debug.Log("Kena damage ! Nyawa tersisa : " + currentLives);

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // apply shift (walk run) modifier *before* movement
            float finalSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkSpeed ;

            // move horizontally
            characterController.Move(moveDir.normalized * finalSpeed * Time.deltaTime);
        }
    
        // gravity & jump
        bool grounded = characterController.isGrounded;
        if (grounded && velocity.y < 0)
        {
            // small downward force to keep grounded contact stable
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // set initial jump velocity upward
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply gravity every frame
        velocity.y += gravity * Time.deltaTime;

        // apply vertical movement separately
        characterController.Move(velocity * Time.deltaTime);

        UpdateAnimation(Mathf.Clamp01(new Vector2(moveX, moveZ).magnitude));
    }

    void UpdateAnimation(float moveInput)
    {
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }
}
