using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    public float jumpForce;
    public float lateralAirborneAcceleration;
    public float maxSpeed;

    private float currentSpeed;
    private float currentJumpForce;

    private float speedWeight = 0;
    private float jumpWeight = 0;

    [HideInInspector]
    public bool active = true;

    private bool grounded = false;
    private Transform groundCheck;

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        currentSpeed = maxSpeed;
        currentJumpForce = jumpForce;
    }

    private void Update()
    {
        grounded = IsGrounded();
    }

    public void Jump()
    {
        if (!enabled) return;

        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    /// <summary>
    /// Méthode appelée par le PlayerController. 
    /// Le mouvement est fait pour être arcade et pas chiant, aka 0 inertie sauf dans les sauts.
    /// </summary>
    /// <param name="magnitude">Axe du stick/bouton pressé</param>
    public void Move(float magnitude)
    {
        if (!enabled) return;
        
        if (magnitude != 0)
        {
            Flip(magnitude);
        }

        if (grounded && magnitude == 0 && GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            StopX();
        }

        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude * currentSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude * lateralAirborneAcceleration);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > currentSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void StopX()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(30, GetComponent<Rigidbody2D>().velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void Flip(float lateralAcceleration)
    {
		Vector3 currentScale = transform.localScale;
		currentScale.x = Mathf.Sign(lateralAcceleration);
		transform.localScale = currentScale;
    }

    public void IncrementSpeedWeight(float increment)
    {
        speedWeight += increment;
        currentSpeed = Mathf.Max(Mathf.Min(maxSpeed, maxSpeed + speedWeight), 0);
    }

    public void IncrementJumpWeight(float increment)
    {
        jumpWeight += increment;
        currentJumpForce = Mathf.Max(Mathf.Min(maxSpeed, jumpForce + jumpWeight), 0);
    }
}
