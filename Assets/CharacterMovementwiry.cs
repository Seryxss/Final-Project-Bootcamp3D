using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovementwiry : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;

    GameObject pickedObject;

    Vector3 velocity;
    private float smoothTime = 0.1f;
    CharacterController characterController;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

            // apply shift modifier *before* movement
            float finalSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * 0.3f : moveSpeed;

            // move horizontally
            characterController.Move(moveDir.normalized * finalSpeed * Time.deltaTime);
        }

        // gravity & jump
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime;

        // apply vertical movement separately
        characterController.Move(velocity * Time.deltaTime);

        UpdateAnimation(Mathf.Clamp01(new Vector2(moveX, moveZ).magnitude));
    }

    void UpdateAnimation(float moveInput)
    {
        animator.SetFloat("speed", Mathf.Abs(Input.GetKey(KeyCode.LeftShift) ? moveInput * 0.3f : moveInput));
        animator.SetBool("isGrounded", characterController.isGrounded);
    }
}
