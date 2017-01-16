using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    public float jumpForce;

    private bool grounded = false;
    private Transform groundCheck;
    private bool jump = false;

    private Vector3 lastPosition = Vector3.zero;
    private float speed = 0f;

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
    }

    private void Update()
    {
        grounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;

        if (jump)
        {
            jump = false;
        }
    }

    void OnGUI()
    {
        //TODO : remove debug info
        GUI.Label(new Rect(10, 10, 100, 20), speed.ToString());
        GUI.Label(new Rect(10, 30, 100, 20), (IsGrounded() ? "grounded" : "not grounded"));
        GUI.Label(new Rect(10, 50, 100, 20), groundCheck.position.ToString());
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
