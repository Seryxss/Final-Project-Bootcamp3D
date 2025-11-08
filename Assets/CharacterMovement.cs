using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    public float turnSpeed = 120f;

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
        //currentLives--;
        //Debug.Log("Kena damage ! Nyawa tersisa : " + currentLives);

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        // float moveY = Input.GetAxis("Vertical");
         float moveZ = Input.GetAxis("Vertical");

        //        Vector3 movement = transform.right * moveX + transform.forward * moveY;
        Vector3 movement = transform.forward * moveZ;
        transform.Rotate(0, moveX * turnSpeed * Time.deltaTime, 0);

        characterController.Move(movement*moveSpeed*Time.deltaTime);
        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("jump");
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        UpdateAnimation(moveZ);
    }

    void UpdateAnimation(float moveInput)
    {
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }
}
