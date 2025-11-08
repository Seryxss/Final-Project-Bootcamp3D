using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");  // A & D
        float moveZ = Input.GetAxis("Vertical");    // W & S

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
