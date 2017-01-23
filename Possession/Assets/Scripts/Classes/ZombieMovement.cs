using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    public float jumpForce;

    private bool grounded = false;
    private Transform groundCheck;

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
    }

    private void Update()
    {
        grounded = IsGrounded();
    }

    public bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void Jump()
    {
        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }
}
